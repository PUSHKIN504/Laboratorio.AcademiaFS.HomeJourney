import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | UrlTree {
    const userData = localStorage.getItem('user');
    if (userData) {
      return true;
    } else {
      Swal.fire({
        title: 'Acceso Denegado',
        text: 'Debe iniciar sesión para acceder a esta página.',
        icon: 'warning',
        confirmButtonText: 'OK'
      }).then(() => {
        this.router.navigate(['/login']);
      });
      return false;
    }
  }
}
