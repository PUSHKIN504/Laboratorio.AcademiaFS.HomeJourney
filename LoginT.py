import httpx
import json
import os
from textual.app import App, ComposeResult
from textual.containers import Horizontal, Vertical
from textual.widgets import Header, Footer, Input, Button, Static, Label
from textual.message import Message
from textual.binding import Binding
from textual.screen import Screen

# Import your existing PaisesApp
from consoleProjectTextual import PaisesScreen

class LoginScreen(Screen):
    """Login screen for the application."""
    
    class LoginSuccessful(Message):
        """Message sent when login is successful."""
        def __init__(self, user_data) -> None:
            super().__init__()
            self.user_data = user_data
    
    BINDINGS = [
        Binding("escape", "app.quit", "Quit"),
    ]
    
    def compose(self) -> ComposeResult:
        """Compose the login form."""
        yield Header()
        with Vertical(id="login_container"):
            yield Static("Iniciar Sesión", id="login_title")
            yield Label("Usuario")
            yield Input(placeholder="Nombre de usuario", id="username_input")
            yield Label("Contraseña")
            yield Input(placeholder="Contraseña", id="password_input", password=True)
            yield Static("", id="login_message", classes="error")
            with Horizontal(id="login_buttons"):
                yield Button("Ingresar", id="login_button", variant="primary")
                yield Button("Cancelar", id="cancel_button", variant="default")
    
    def on_button_pressed(self, event: Button.Pressed) -> None:
        """Handle button press events."""
        if event.button.id == "login_button":
            self.run_worker(self.handle_login())
        elif event.button.id == "cancel_button":
            self.clear_form()
    
    def on_input_submitted(self, event: Input.Submitted) -> None:
        """Handle input submission (pressing Enter in a field)."""
        self.run_worker(self.handle_login())
    
    async def handle_login(self) -> None:
        """Process the login request."""
        username_input = self.query_one("#username_input", Input)
        password_input = self.query_one("#password_input", Input)
        login_message = self.query_one("#login_message", Static)
        
        username = username_input.value.strip()
        password = password_input.value.strip()
        
        # Basic validation
        if not username or not password:
            login_message.update("Error: Usuario y contraseña son requeridos")
            return
        
        # Clear error message
        login_message.update("")
        
        # Attempt login with API
        login_button = self.query_one("#login_button", Button)
        login_button.disabled = True
        login_button.label = "Procesando..."
        
        try:
            async with httpx.AsyncClient() as client:
                url = "http://localhost:36955/academiafarsiman/Usuarios/login"
                payload = {
                    "username": username,
                    "password": password
                }
                
                response = await client.post(url, json=payload)
                
                if response.status_code == 200:
                    response_data = response.json()
                    # Store user session data
                    if "data" in response_data:
                        self.save_session(response_data["data"])
                        self.post_message(self.LoginSuccessful(response_data["data"]))
                    else:
                        login_message.update("Error: Formato de respuesta inválido")
                else:
                    error_text = "Usuario o contraseña incorrectos"
                    try:
                        error_data = response.json()
                        if "message" in error_data:
                            error_text = error_data["message"]
                    except:
                        pass
                    login_message.update(f"Error: {error_text}")
        except Exception as e:
            login_message.update(f"Error de conexión: {str(e)}")
        finally:
            login_button.disabled = False
            login_button.label = "Ingresar"
    
    def clear_form(self) -> None:
        """Clear the form fields."""
        username_input = self.query_one("#username_input", Input)
        password_input = self.query_one("#password_input", Input)
        login_message = self.query_one("#login_message", Static)
        
        username_input.value = ""
        password_input.value = ""
        login_message.update("")
    
    def save_session(self, user_data) -> None:
        """Save session information."""
        # Create a simple session file
        try:
            with open("session.json", "w") as f:
                json.dump(user_data, f)
        except Exception as e:
            print(f"Error saving session: {e}")


class MainApp(App):
    """Main application with login and management screens."""
    
    CSS = """
    #login_container {
        width: 60;
        height: 80%;
        margin: 1;
        align: center middle;
        padding: 1;
        border: round #00FF00;
        background: #222222;
    }
    
    #login_title {
        padding: 1;
        margin-bottom: 2;
        content-align: center middle;
        background: #444444;
        color: white;
        text-style: bold;
    }
    
    Label {
        margin-left: 1;
        margin-top: 1;
    }
    
    Input {
        margin: 1;
    }
    
    #login_buttons {
        margin-top: 2;
        content-align: center middle;
    }
    
    Button {
        margin: 1;
    }
    
    #login_message {
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
    
    /* Inherit all the CSS from PaisesApp */
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
    #logout_button {
            dock: right;
            margin: 1 2;
            background: $error;
            color: $text;
        }

    /* Si quieres que el botón cambie de color al pasar el mouse por encima */
        #logout_button:hover {
            background: $error-darken-2;
        }
    """
    
    BINDINGS = [
        Binding("d", "toggle_dark", "Toggle dark mode"),
        Binding("ctrl+q", "app.quit", "Quit"),
    ]
    
    def __init__(self):
        super().__init__()
        self.user_data = None
        self.check_existing_session()
    
    def check_existing_session(self):
        """Check if there's an existing session."""
        try:
            if os.path.exists("session.json"):
                with open("session.json", "r") as f:
                    self.user_data = json.load(f)
        except Exception:
            self.user_data = None
    
    def on_mount(self) -> None:
        """When app is mounted, check if we should go directly to main screen."""
        if self.user_data:
            self.go_to_main_screen()
        else:
            self.push_screen(LoginScreen())
    
    def on_login_screen_login_successful(self, message: LoginScreen.LoginSuccessful) -> None:
        """Handle successful login."""
        self.user_data = message.user_data
        self.go_to_main_screen()
    
    # def go_to_main_screen(self) -> None:
    #     """Switch to the main paises management screen."""
    #     paises_app = PaisesScreen()
    #     self.notify("Login exitoso", severity="success")
    #     # You could pass user_data to PaisesApp if needed for authorization headers etc.
    #     self.switch_screen(paises_app)
    def go_to_main_screen(self) -> None:
        """Switch to the main paises management screen."""
        self.notify("Login exitoso", severity="success")
        paises_screen = PaisesScreen()
        name="paises"
        if name in self._installed_screens: 
            self.push_screen("paises")
            return
        self.install_screen(paises_screen, name )
        self.push_screen("paises")
        
    def logout(self) -> None:
        """Handle user logout."""
        self.user_data = None
        try:
            if os.path.exists("session.json"):
                os.remove("session.json")
        except Exception as e:
            print(f"Error removing session file: {e}")
        self.push_screen(LoginScreen())


if __name__ == "__main__":
    app = MainApp()
    app.run()