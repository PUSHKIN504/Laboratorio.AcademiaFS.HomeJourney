import { Routes } from '@angular/router';
import { PaisComponent } from './views/components/pais/pais.component';
import { DirectivasComponent } from './views/directives/directivas.component';
import { ColaboradoresComponent } from './views/pages/generals/colaboradores/colaboradores.component';
import { LoginComponent } from './views/pages/auth/login/login.component';
export const routes: Routes = [
  { path: 'Paises', component: PaisComponent },
  { path: 'Directivas', component: DirectivasComponent },
  { path: 'Colaboradores', component: ColaboradoresComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'inicio', pathMatch: 'full' },
  { path: '**', redirectTo: 'inicio' }
];
