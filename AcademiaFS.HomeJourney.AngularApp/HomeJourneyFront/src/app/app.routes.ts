import { Routes } from '@angular/router';
import { PaisComponent } from './views/components/pais/pais.component';
import { DirectivasComponent } from './views/directives/directivas.component';
import { ColaboradoresComponent } from './views/pages/generals/colaboradores/colaboradores.component';
import { LoginComponent } from './views/pages/auth/login/login.component';
import { ColaboradoresSucursalesComponent } from './views/pages/generals/colaboradores-sucursales/colaboradores-sucursales.component';
import { ViajesClusteredComponent } from './views/pages/trips/generarviaje/generarviaje.component';
import { AuthGuard } from '../../@helpers/guards/auth.guard';
import { ManagerGuard } from '../../@helpers/guards/mager.guard';
export const routes: Routes = [

    { path: 'login', component: LoginComponent },

    { path: 'Paises', component: PaisComponent, canActivate: [AuthGuard] },
    { path: 'Directivas', component: DirectivasComponent, canActivate: [AuthGuard] },
    { path: 'Colaboradores', component: ColaboradoresComponent, canActivate: [AuthGuard, ManagerGuard] },
    { path: 'ColaboradoresSucursales', component: ColaboradoresSucursalesComponent, canActivate: [AuthGuard, ManagerGuard] },
    { path: 'Generarviaje', component: ViajesClusteredComponent, canActivate: [AuthGuard, ManagerGuard] },
  
    { path: '', redirectTo: 'inicio', pathMatch: 'full' },
    { path: '**', redirectTo: 'inicio' }
];
