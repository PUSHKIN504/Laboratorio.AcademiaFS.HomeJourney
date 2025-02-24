import httpx
from textual.app import App, ComposeResult
from textual.containers import Horizontal, Vertical
from textual.widgets import Header, Footer, DataTable, Input, Button, Static
from textual.message import Message
from textual.binding import Binding

class CreateCargoForm(Vertical):
    class CargoCreated(Message):
        def __init__(self, nombre: str, activo: bool, cargo_id: int = 0) -> None:
            super().__init__()
            self.nombre = nombre
            self.activo = activo
            self.cargo_id = cargo_id

    class CargoDeleted(Message):
        def __init__(self, cargo_id: int) -> None:
            super().__init__()
            self.cargo_id = cargo_id

    class CargoStatusChanged(Message):
        def __init__(self, cargo_id: int, activo: bool) -> None:
            super().__init__()
            self.cargo_id = cargo_id
            self.activo = activo

    def compose(self) -> ComposeResult:
        yield Static("Gestionar Cargo", id="form_title")
        yield Input(placeholder="ID del cargo", id="id_input", disabled=True)
        yield Input(placeholder="Nombre del cargo", id="nombre_input")
        yield Input(placeholder="Activo (true/false)", id="activo_input")
        yield Static("", id="validation_message", classes="error")
        with Horizontal():
            yield Button("Crear", id="create_button", variant="primary")
            yield Button("Actualizar", id="update_button", variant="warning", disabled=True)
            yield Button("Eliminar", id="delete_button", variant="error", disabled=True)
            yield Button("Cambiar estado", id="toggle_status_button", variant="primary", disabled=True)
            yield Button("Limpiar", id="clear_button", variant="default")

    def on_button_pressed(self, event: Button.Pressed) -> None:
        if event.button.id in ["create_button", "update_button"]:
            id_input = self.query_one("#id_input", Input)
            nombre_input = self.query_one("#nombre_input", Input)
            activo_input = self.query_one("#activo_input", Input)
            validation_message = self.query_one("#validation_message", Static)

            nombre = nombre_input.value.strip()
            if not nombre:
                validation_message.update("Error: El nombre del cargo no puede estar vacío")
                return

            activo_value = activo_input.value.strip().lower()
            if not activo_value or activo_value not in ["true", "false", "1", "0", "yes", "no"]:
                validation_message.update("Error: El estado activo debe ser true/false")
                return

            validation_message.update("")
            activo = activo_value in ["true", "1", "yes"]
            cargo_id = int(id_input.value) if id_input.value.strip().isdigit() else 0

            self.post_message(CreateCargoForm.CargoCreated(nombre, activo, cargo_id))
            self.clear_form()

        elif event.button.id == "delete_button":
            id_input = self.query_one("#id_input", Input)
            validation_message = self.query_one("#validation_message", Static)
            cargo_id = id_input.value.strip()
            if not cargo_id.isdigit():
                validation_message.update("Error: ID del cargo inválido")
                return
            validation_message.update("")
            self.post_message(CreateCargoForm.CargoDeleted(int(cargo_id)))
            self.clear_form()

        elif event.button.id == "toggle_status_button":
            id_input = self.query_one("#id_input", Input)
            activo_input = self.query_one("#activo_input", Input)
            validation_message = self.query_one("#validation_message", Static)
            cargo_id = id_input.value.strip()
            if not cargo_id.isdigit():
                validation_message.update("Error: ID del cargo inválido")
                return
            current_status = activo_input.value.strip().lower() in ["true", "1", "yes"]
            new_status = not current_status
            validation_message.update("")
            self.post_message(CreateCargoForm.CargoStatusChanged(int(cargo_id), new_status))

        elif event.button.id == "clear_button":
            self.clear_form()

    def clear_form(self) -> None:
        id_input = self.query_one("#id_input", Input)
        nombre_input = self.query_one("#nombre_input", Input)
        activo_input = self.query_one("#activo_input", Input)
        validation_message = self.query_one("#validation_message", Static)
        create_button = self.query_one("#create_button", Button)
        update_button = self.query_one("#update_button", Button)
        delete_button = self.query_one("#delete_button", Button)
        toggle_status_button = self.query_one("#toggle_status_button", Button)

        id_input.value = ""
        nombre_input.value = ""
        activo_input.value = ""
        validation_message.update("")
        create_button.disabled = False
        update_button.disabled = True
        delete_button.disabled = True
        toggle_status_button.disabled = True
        toggle_status_button.label = "Cambiar estado"

    def load_cargo_data(self, cargo_id: str, nombre: str, activo: str) -> None:
        id_input = self.query_one("#id_input", Input)
        nombre_input = self.query_one("#nombre_input", Input)
        activo_input = self.query_one("#activo_input", Input)
        validation_message = self.query_one("#validation_message", Static)
        create_button = self.query_one("#create_button", Button)
        update_button = self.query_one("#update_button", Button)
        delete_button = self.query_one("#delete_button", Button)
        toggle_status_button = self.query_one("#toggle_status_button", Button)

        id_input.value = cargo_id
        nombre_input.value = nombre
        activo_input.value = "true" if activo == "Sí" else "false"
        validation_message.update("")
        create_button.disabled = True
        update_button.disabled = False
        delete_button.disabled = False
        toggle_status_button.disabled = False
        if activo == "Sí":
            toggle_status_button.label = "Desactivar"
        else:
            toggle_status_button.label = "Activar"


class CargosApp(App):
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
    #validation_message {
        color: #FF0000;
        background: #FFEEEE;
        padding: 1;
        margin: 1;
        display: block;
        height: auto;
    }
    .error {
        border: solid red;
        background: #FFEEEE;
    }
    """

    BINDINGS = [
        ("d", "toggle_dark", "Toggle dark mode"),
        ("ctrl+o", "logout", "Cerrar sesión"),
    ]

    def compose(self) -> ComposeResult:
        yield Header()
        with Horizontal():
            with Vertical():
                yield Static("Listado de Cargos", id="title")
                self.table = DataTable(id="cargos_table")
                self.table.add_column("ID")
                self.table.add_column("Nombre")
                self.table.add_column("Activo")
                yield self.table
            self.form = CreateCargoForm()
            yield self.form
        yield Footer()

    async def on_mount(self) -> None:
        await self.load_data()

    async def load_data(self) -> None:
        url = "http://localhost:36955/academiafarsiman/cargos"
        try:
            async with httpx.AsyncClient() as client:
                response = await client.get(url)
                response.raise_for_status()
                response_json = response.json()
                self.table.clear()
                if response_json.get("success"):
                    cargos = response_json.get("data", [])
                    for cargo in cargos:
                        self.table.add_row(
                            str(cargo.get("cargoId", "")),
                            cargo.get("nombre", ""),
                            "Sí" if cargo.get("activo", False) else "No"
                        )
                else:
                    self.table.add_row("Error", response_json.get("message", "Error al obtener datos"), "")
        except Exception as e:
            self.table.add_row("Error", str(e), "")

    async def on_data_table_cell_selected(self, event: DataTable.CellSelected) -> None:
        row_index = event.coordinate.row
        row = self.table.get_row_at(row_index)
        if row:
            self.form.load_cargo_data(row[0], row[1], row[2])

    async def on_create_cargo_form_cargo_created(self, message: CreateCargoForm.CargoCreated) -> None:
        url = "http://localhost:36955/academiafarsiman/cargos"
        payload = {
            "cargoId": message.cargo_id,
            "nombre": message.nombre,
            "activo": message.activo
        }
        try:
            async with httpx.AsyncClient() as client:
                if message.cargo_id == 0:
                    response = await client.post(url, json=payload)
                else:
                    response = await client.put(f"{url}/{message.cargo_id}", json=payload)
                response_json = response.json()
                response.raise_for_status()
                if response_json.get("success"):
                    self.notify("Cargo procesado correctamente", severity="information")
                else:
                    self.notify(response_json.get("message", "Error al procesar cargo"), severity="error")
                    return
                await self.load_data()
        except Exception as e:
            self.notify(f"Error: {str(e)}", severity="error")
            print(f"Error al procesar cargo: {e}")

    async def on_create_cargo_form_cargo_status_changed(self, message: CreateCargoForm.CargoStatusChanged) -> None:
        url = f"http://localhost:36955/academiafarsiman/cargos/{message.cargo_id}"
        try:
            async with httpx.AsyncClient() as client:
                response = await client.patch(f"{url}?active={str(message.activo).lower()}")
                response.raise_for_status()
                response_json = response.json()
                if response_json.get("success"):
                    await self.load_data()
                    toggle_button = self.form.query_one("#toggle_status_button", Button)
                    activo_input = self.form.query_one("#activo_input", Input)
                    activo_input.value = "true" if message.activo else "false"
                    toggle_button.label = "Desactivar" if message.activo else "Activar"
                    self.notify(response_json.get("message", "Estado actualizado"), severity="information")
                else:
                    self.notify(response_json.get("message", "Error al cambiar estado"), severity="error")
        except Exception as e:
            self.notify(f"Error al cambiar estado: {str(e)}", severity="error")
            print(f"Error al cambiar estado del cargo: {e}")

    async def on_create_cargo_form_cargo_deleted(self, message: CreateCargoForm.CargoDeleted) -> None:
        url = f"http://localhost:36955/academiafarsiman/cargos/{message.cargo_id}"
        try:
            async with httpx.AsyncClient() as client:
                response = await client.delete(url)
                response_json = response.json()
                if response_json.get("success"):
                    await self.load_data()
                    self.notify("Cargo eliminado correctamente", severity="warning")
                else:
                    self.notify(response_json.get("message", "Error al eliminar cargo"), severity="error")
        except Exception as e:
            self.notify(f"Error al eliminar cargo: {str(e)}", severity="error")
            print(f"Error al eliminar cargo: {e}")

    def action_logout(self) -> None:
        self.app.logout()

if __name__ == "__main__":
    app = CargosApp()
    app.run()
