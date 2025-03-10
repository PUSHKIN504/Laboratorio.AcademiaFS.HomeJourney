// app.routes.ts
import { Routes } from '@angular/router';
import { PaisComponent } from './views/components/pais/pais.component';
import { DirectivasComponent } from './views/directives/directivas.component';

export const routes: Routes = [
  { path: 'Paises', component: PaisComponent },
  { path: 'Directivas', component: DirectivasComponent },
  { path: '', redirectTo: 'inicio', pathMatch: 'full' },
  { path: '**', redirectTo: 'inicio' }
];
