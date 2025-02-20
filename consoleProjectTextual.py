import httpx
from textual.app import App, ComposeResult
from textual.containers import Horizontal, Vertical
from textual.widgets import Header, Footer, DataTable, Input, Button, Static
from textual.message import Message
from textual.binding import Binding


class CreatePaisForm(Vertical):
    class PaisCreated(Message):
        def __init__(self, nombre: str, activo: bool, pais_id: int = 0) -> None:
            super().__init__()  
            self.nombre = nombre
            self.activo = activo
            self.pais_id = pais_id

    class PaisDeleted(Message):
        def __init__(self, pais_id: int) -> None:
            super().__init__()
            self.pais_id = pais_id
            
    class PaisStatusChanged(Message):
        def __init__(self, pais_id: int, activo: bool) -> None:
            super().__init__()
            self.pais_id = pais_id
            self.activo = activo

    def compose(self) -> ComposeResult:
        yield Static("Gestionar País", id="form_title")
        yield Input(placeholder="ID del país", id="id_input", disabled=True)
        yield Input(placeholder="Nombre del país", id="nombre_input")
        yield Input(placeholder="Activo (true/false)", id="activo_input")
        with Horizontal():
            yield Button("Crear", id="create_button", variant="primary")
            yield Button("Actualizar", id="update_button", variant="warning", disabled=True)
            yield Button("Eliminar", id="delete_button", variant="error", disabled=True)
            yield Button("Limpiar", id="clear_button", variant="default")

    def on_button_pressed(self, event: Button.Pressed) -> None:
        if event.button.id in ["create_button", "update_button"]:
            id_input = self.query_one("#id_input", Input)
            nombre_input = self.query_one("#nombre_input", Input)
            activo_input = self.query_one("#activo_input", Input)
            
            nombre = nombre_input.value.strip()
            activo = activo_input.value.strip().lower() in ["true", "1", "yes"]
            pais_id = int(id_input.value) if id_input.value.strip().isdigit() else 0
            
            self.post_message(CreatePaisForm.PaisCreated(nombre, activo, pais_id))
            self.clear_form()
        
        elif event.button.id == "delete_button":
            id_input = self.query_one("#id_input", Input)
            pais_id = int(id_input.value) if id_input.value.strip().isdigit() else 0
            if pais_id:
                self.post_message(CreatePaisForm.PaisDeleted(pais_id))
                self.clear_form()

        elif event.button.id == "clear_button":
            self.clear_form()

    def clear_form(self) -> None:
        """Limpia todos los campos del formulario y restablece los botones"""
        id_input = self.query_one("#id_input", Input)
        nombre_input = self.query_one("#nombre_input", Input)
        activo_input = self.query_one("#activo_input", Input)
        create_button = self.query_one("#create_button", Button)
        update_button = self.query_one("#update_button", Button)
        delete_button = self.query_one("#delete_button", Button)
        toggle_status_button = self.query_one("#toggle_status_button", Button)

        id_input.value = ""
        nombre_input.value = ""
        activo_input.value = ""
        create_button.disabled = False
        toggle_status_button.disabled = True
        update_button.disabled = True
        delete_button.disabled = True
        toggle_status_button.label = "Desactivar"

    def load_pais_data(self, pais_id: str, nombre: str, activo: str) -> None:
        """Carga los datos de un país en el formulario"""
        id_input = self.query_one("#id_input", Input)
        nombre_input = self.query_one("#nombre_input", Input)
        activo_input = self.query_one("#activo_input", Input)
        create_button = self.query_one("#create_button", Button)
        update_button = self.query_one("#update_button", Button)
        delete_button = self.query_one("#delete_button", Button)
        toggle_status_button = self.query_one("#toggle_status_button", Button)

        id_input.value = pais_id
        nombre_input.value = nombre
        activo_input.value = "true" if activo == "Sí" else "false"
        
        create_button.disabled = True
        update_button.disabled = False
        delete_button.disabled = False
        toggle_status_button.disabled = False
        
        if activo == "Sí":
            toggle_status_button.label = "Desactivar"
        else:
            toggle_status_button.label = "Activar"


class PaisesApp(App):
    CSS = """
    #title {
        padding: 1;
        content-align: center middle;
        background: #444444;
        color: white;
    }
    DataTable {
        margin: 2;
        border: round #00FF00;
    }
    #form_title {
        padding: 1;
        content-align: center middle;
        background: #444444;
        color: white;
    }
    Input {
        margin: 1;
    }
    Button {
        margin: 1;
    }
    Horizontal {
        height: auto;
        margin: 1;
    }
    """

    BINDINGS = [
        Binding("d", "toggle_dark", "Toggle dark mode"),
    ]

    def compose(self) -> ComposeResult:
        yield Header()
        with Horizontal():
            with Vertical():
                yield Static("Listado de Países", id="title")
                self.table = DataTable(id="paises_table")
                self.table.add_column("ID")
                self.table.add_column("Nombre")
                self.table.add_column("Activo")
                yield self.table
            self.form = CreatePaisForm()
            yield self.form
        yield Footer()

    async def on_mount(self) -> None:
        await self.load_data()

    async def load_data(self) -> None:
        url = "http://localhost:36955/academiafarsiman/paises"
        try:
            async with httpx.AsyncClient() as client:
                response = await client.get(url)
                response.raise_for_status()
                response_json = response.json()
                self.table.clear()
                if response_json.get("success"):
                    paises = response_json.get("data", [])
                    for pais in paises:
                        self.table.add_row(
                            str(pais.get("paisId", "")),
                            pais.get("nombre", ""),
                            "Sí" if pais.get("activo", False) else "No"
                        )
                else:
                    self.table.add_row("Error", response_json.get("message", "Error al obtener datos"), "")
        except Exception as e:
            self.table.add_row("Error", str(e), "")

    async def on_data_table_cell_selected(self, event: DataTable.CellSelected) -> None:
        """Cuando se selecciona una celda, se cargan los datos en el formulario para editar"""
        row_index = event.coordinate.row
        row = self.table.get_row_at(row_index)
        if row:
            self.form.load_pais_data(row[0], row[1], row[2])

    async def on_create_pais_form_pais_created(self, message: CreatePaisForm.PaisCreated) -> None:
        url = "http://localhost:36955/academiafarsiman/paises"
        payload = {
            "paisId": message.pais_id,
            "nombre": message.nombre,
            "activo": message.activo
        }
        
        try:
            async with httpx.AsyncClient() as client:
                if message.pais_id == 0:
                    response = await client.post(url, json=payload)
                    self.notify("País creado correctamente", severity="information")
                else:
                    response = await client.put(url + f"/{message.pais_id}", json=payload)
                    self.notify("País actualizado correctamente", severity="information")

                response.raise_for_status()
                await self.load_data()
        except Exception as e:
            print(f"Error al procesar país: {e}")

    async def on_create_pais_form_pais_status_changed(self, message: CreatePaisForm.PaisStatusChanged) -> None:
        url = f"http://localhost:36955/academiafarsiman/paises/{message.pais_id}/activo?active={str(message.activo).lower()}"
        
        try:
            async with httpx.AsyncClient() as client:
                response = await client.patch(url)
                response.raise_for_status()
                await self.load_data()
                estado_texto = "activado" if message.activo else "desactivado"
                self.notify(f"País {estado_texto} correctamente", severity="warning")
        except Exception as e:
            print(f"Error al cambiar el estado del país: {e}")

    async def on_create_pais_form_pais_deleted(self, message: CreatePaisForm.PaisDeleted) -> None:
        url = f"http://localhost:36955/academiafarsiman/paises/{message.pais_id}"
        
        try:
            async with httpx.AsyncClient() as client:
                response = await client.delete(url)
                response.raise_for_status()
                await self.load_data()
                self.notify("País eliminado correctamente", severity="warning")
        except Exception as e:
            print(f"Error al eliminar país: {e}")

if __name__ == "__main__":
    app = PaisesApp()
    app.run()
