import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import {  UsuarioLoginRequest, UsuarioDto } from '../../../models/user.model';
import { AuthBaseService } from '../../../services/login.service';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSnackBarModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  user: string = '';
  password: string = '';
  loginValid: boolean = true;
  year: number = new Date().getFullYear();
  currentYear: number = new Date().getFullYear();
  private authService: AuthBaseService = new AuthBaseService("academiafarsiman/Usuarios");

  constructor(private router: Router, private snackBar: MatSnackBar) {}

  login(): void {
    const request: UsuarioLoginRequest = { username: this.user, password: this.password };

    this.authService.login(request)
      .then((response: UsuarioDto) => {
        this.loginValid = true;
        this.snackBar.open('Login exitoso', 'Cerrar', { duration: 3000 });
        this.router.navigate(['index']);
      })
      .catch((error: any) => {
        this.loginValid = false;
        this.snackBar.open('El usuario y contraseña no son correctos!', 'Cerrar', { duration: 3000 });
      });
  }
}
