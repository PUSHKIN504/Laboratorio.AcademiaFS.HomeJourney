import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { DirectivasComponent } from '../../views/directives/directivas.component';


import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

interface MenuItem {
  label: string;
  icon?: string;
  route?: string;
  children?: MenuItem[];
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatExpansionModule
  ],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  showSidebar: boolean = true;
  constructor(private router: Router) {}
  

    ngOnInit(): void {
      this.updateSidebarVisibility();
      console.log('SidebarComponent iniciado'+ this.showSidebar);
      this.router.events
        .pipe(filter(event => event instanceof NavigationEnd))
        .subscribe(() => {
          this.updateSidebarVisibility();
        });
    }
    updateSidebarVisibility(): void {
      this.showSidebar = !!localStorage.getItem('user');
    }
    logout(): void {
      localStorage.removeItem('user');
      this.router.navigate(['/login']);
    }
  menuItems: MenuItem[] = [
    { label: 'Inicio', icon: 'home', route: '/inicio' },
    {
      label: 'Generales',
      icon: 'menu',
      children: [
        // { label: 'Paises', route: '/Paises' },
        // { label: 'Test Directivas', route: '/Directivas' },
        { label: 'Colaboradores', route: '/Colaboradores' },
        { label: 'Asignación Colaboradores', route: '/ColaboradoresSucursales' },
        { label: 'Generar Viajes', route: '/Generarviaje' },
        // { label: 'Otra Página', route: '/otra' },
        
      ]
    },
  ];
}
