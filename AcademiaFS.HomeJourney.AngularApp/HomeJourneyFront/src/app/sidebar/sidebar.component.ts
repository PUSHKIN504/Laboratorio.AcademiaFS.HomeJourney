import { Component } from '@angular/core';

interface MenuItem {
  label: string;
  icon?: string;
  route?: string;
  children?: MenuItem[];
}

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  menuItems: MenuItem[] = [
    { label: 'Inicio', icon: 'home', route: '/inicio' },
    {
      label: 'Opciones',
      icon: 'menu',
      children: [
        { label: 'Test Page', route: '/testpage' },
        { label: 'Otra Página', route: '/otra' }
      ]
    },
    { label: 'Contacto', icon: 'contact_mail', route: '/contacto' }
  ];
}
