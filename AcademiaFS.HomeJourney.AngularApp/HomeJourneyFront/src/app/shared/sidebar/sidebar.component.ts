// sidebar.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { DirectivasComponent } from '../../views/directives/directivas.component';

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
export class SidebarComponent {
  menuItems: MenuItem[] = [
    { label: 'Inicio', icon: 'home', route: '/inicio' },
    {
      label: 'Generales',
      icon: 'menu',
      children: [
        { label: 'Paises', route: '/Paises' },
        { label: 'Test Directivas', route: '/Directivas' },
        { label: 'Otra Página', route: '/otra' },
        
      ]
    },
  ];
}
