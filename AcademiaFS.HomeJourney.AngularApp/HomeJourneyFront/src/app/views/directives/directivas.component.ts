import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-directivas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './directivas.component.html',
  styleUrls: ['./directivas.component.scss']
})
export class DirectivasComponent {
  showForm: boolean = true;
  highlight: boolean = false;
  sucursales: string[] = ['Sucursal 1', 'Sucursal 2', 'Sucursal 3'];
  toggleForm(): void {
    this.showForm = !this.showForm;
  }
  toggleHighlight(): void {
    this.highlight = !this.highlight;
  }
}
