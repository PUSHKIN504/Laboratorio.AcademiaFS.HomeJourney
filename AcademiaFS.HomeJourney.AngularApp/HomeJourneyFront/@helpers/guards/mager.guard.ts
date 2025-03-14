import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class ManagerGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean | UrlTree {
    const userDataStr = localStorage.getItem('user');
    if (!userDataStr) {
      Swal.fire({
        title: 'Acceso denegado',
        text: 'Debe iniciar sesión para acceder a esta sección.',
        icon: 'warning',
        confirmButtonText: 'OK'
      });
      return this.router.parseUrl('/login');
    }
    try {
      const user = JSON.parse(userDataStr);
      if (user.data && (user.data.cargo === 'Gerente Tienda' || user.data.rol === 'Gerente Tienda')) {
        return true;
      } else {
        Swal.fire({
          title: 'Acceso denegado',
          text: 'No tiene permisos para acceder a esta sección.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
        return this.router.parseUrl('/inicio');
      }
    } catch (error) {
      Swal.fire({
        title: 'Error',
        text: 'Error al procesar la información del usuario. Por favor, inicie sesión nuevamente.',
        icon: 'error',
        confirmButtonText: 'OK'
      });
      return this.router.parseUrl('/login');
    }
  }
}
