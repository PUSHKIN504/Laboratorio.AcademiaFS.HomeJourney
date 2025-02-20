import httpx
from textual.app import App, ComposeResult
from textual.containers import Horizontal, Vertical
from textual.widgets import Header, Footer, DataTable, Input, Button, Static
from textual.message import Message

class CreatePaisForm(Vertical):
    class PaisCreated(Message):
        def __init__(self, nombre: str, activo: bool) -> None:
            super().__init__()
            self.nombre = nombre
            self.activo = activo

    def compose(self) -> ComposeResult:
        yield Static("Crear Nuevo País", id="form_title")
        yield Input(placeholder="Nombre del país", id="nombre_input")
        yield Input(placeholder="Activo (true/false)", id="activo_input")
        yield Button("Crear", id="create_button")

    def on_button_pressed(self, event: Button.Pressed) -> None:
        if event.button.id == "create_button":
            nombre_input = self.query_one("#nombre_input", Input)
            activo_input = self.query_one("#activo_input", Input)
            nombre = nombre_input.value.strip()
            activo = activo_input.value.strip().lower() in ["true", "1", "yes"]
            self.post_message(self.PaisCreated(nombre, activo))
            # Limpiar los inputs después de enviar el mensaje
            nombre_input.value = ""
            activo_input.value = ""


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
    """

    def compose(self) -> ComposeResult:
        yield Header()
        with Horizontal():
            with Vertical():
                yield Static("Listado de Países", id="title")
                self.table = DataTable(id="paises_table")
                self.table.add_column("ID", width=6)
                self.table.add_column("Nombre", width=20)
                self.table.add_column("Activo", width=8)
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

    async def on_create_pais_form_pais_created(self, message: CreatePaisForm.PaisCreated) -> None:
        url = "http://localhost:36955/academiafarsiman/paises"
        payload = {"paisId": 0, "nombre": message.nombre, "activo": message.activo}
        try:
            async with httpx.AsyncClient() as client:
                response = await client.post(url, json=payload)
                response.raise_for_status()
                # Recargar los datos después de una inserción exitosa
                await self.load_data()
                self.notify("País creado correctamente", severity="information")
        except Exception as e:
            self.notify(f"Error al crear país: {e}", severity="error")


if __name__ == "__main__":
    app = PaisesApp()
    app.run()