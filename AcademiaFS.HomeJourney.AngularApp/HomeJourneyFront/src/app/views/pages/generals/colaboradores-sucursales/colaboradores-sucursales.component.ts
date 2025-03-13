import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import {
  DxSelectBoxModule,
  DxNumberBoxModule,
  DxButtonModule,
  DxDataGridModule
} from 'devextreme-angular';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DxiColumnModule } from 'devextreme-angular/ui/nested';

interface Colaborador {
  personaId: number;
  colaboradorId: number;
  nombre: string;
  apelllido: string;
}

interface Sucursal {
  sucursalId: number;
  nombre: string;
  direccion: string;
}

export interface ColaboradorSucursalDto {
  colaboradorsucursalId?: number;
  colaboradorId: number;
  sucursalId: number;
  distanciaKilometro: number;
  activo: boolean;
  usuarioCrea: number;
  fechaCrea: Date;
  usuarioModifica?: number;
  fechaModifica?: Date;
}

@Component({
  selector: 'app-colaborador-sucursales',
  templateUrl: './colaboradores-sucursales.component.html',
  styleUrls: ['./colaboradores-sucursales.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DxSelectBoxModule,
    DxNumberBoxModule,
    DxButtonModule,
    DxDataGridModule,
    DxiColumnModule,
    MatToolbarModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule
  ]
})
export class ColaboradorSucursalesComponent implements OnInit {
  // Usamos _form para ajustarse al patrón de tu componente genérico
  _form: FormGroup;
  // Variables para la asignación
  colaboradores: Colaborador[] = [];
  sucursales: Sucursal[] = [];
  assignedList: ColaboradorSucursalDto[] = [];
  // Objeto popupOptions para usar en el custom-form
  popupOptions = {
    title: 'Asignar Sucursal a Colaborador',
    visible: false,
    loading: false
  };

  // Configuración para los select boxes
  collaboratorSelectOptions: any;
  sucursalSelectOptions: any;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private http: HttpClient
  ) {
    this._form = this.fb.group({
      colaboradorId: [null, Validators.required],
      sucursalId: [null, Validators.required],
      distanciaKilometro: [null, [Validators.required, Validators.min(0.01), Validators.max(50)]]
    });
  }

  ngOnInit(): void {
    this.loadColaboradores();
    this.loadSucursales();
    this.getAssigned();
  }

  loadColaboradores(): void {
    // Se asume que el endpoint devuelve { data: Colaborador[] }
    this.http.get<any>('http://localhost:36955/academiafarsiman/personascolaboradores').subscribe({
      next: (res) => {
        this.colaboradores = res.data;
        this.collaboratorSelectOptions = {
          dataSource: this.colaboradores,
          displayExpr: this.getColaboradorDisplayName.bind(this),
          valueExpr: 'personaId',
          placeholder: 'Seleccione colaborador'
        };
      },
      error: () => {
        this.snackBar.open('Error al cargar colaboradores', 'Cerrar', { duration: 3000 });
      }
    });
  }

  loadSucursales(): void {
    // Se asume que el endpoint devuelve { data: Sucursal[] }
    this.http.get<any>('http://localhost:36955/academiafarsiman/sucursales/raw').subscribe({
      next: (res) => {
        this.sucursales = res.data;
        this.sucursalSelectOptions = {
          dataSource: this.sucursales,
          displayExpr: 'nombre',
          valueExpr: 'sucursalId',
          placeholder: 'Seleccione sucursal'
        };
      },
      error: () => {
        this.snackBar.open('Error al cargar sucursales', 'Cerrar', { duration: 3000 });
      }
    });
  }

  getAssigned(): void {
    // Se asume que el endpoint devuelve { data: ColaboradorSucursalDto[] }
    this.http.get<any>('http://localhost:36955/academiafarsiman/colaboradoressucursales').subscribe({
      next: (res) => {
        this.assignedList = res.data;
      },
      error: () => {
        this.snackBar.open('Error al cargar asignaciones', 'Cerrar', { duration: 3000 });
      }
    });
  }

  openPopup(): void {
    this.popupOptions.visible = true;
    this._form.reset();
  }

  onClosePopup(): void {
    this.popupOptions.visible = false;
  }

  onSave(): void {
    if (this._form.invalid) {
      this.snackBar.open('Complete todos los campos y verifique que la distancia esté entre 0 y 50 km', 'Cerrar', { duration: 3000 });
      return;
    }
    const formValue = this._form.value as ColaboradorSucursalDto;
    // Validar que el colaborador no tenga asignada la misma sucursal
    const exists = this.assignedList.some(
      asg => asg.colaboradorId === formValue.colaboradorId && asg.sucursalId === formValue.sucursalId
    );
    if (exists) {
      this.snackBar.open('Este colaborador ya tiene asignada esta sucursal.', 'Cerrar', { duration: 3000 });
      return;
    }
    // Completar valores adicionales
    formValue.activo = true;
    formValue.usuarioCrea = 1; // Ajusta según el usuario actual
    formValue.fechaCrea = new Date();

    this.http.post<any>('http://localhost:36955/academiafarsiman/colaboradoressucursales', formValue).subscribe({
      next: () => {
        this.snackBar.open('Sucursal asignada correctamente', 'Cerrar', { duration: 3000 });
        this.getAssigned();
        this.onClosePopup();
      },
      error: () => {
        this.snackBar.open('Error al asignar sucursal', 'Cerrar', { duration: 3000 });
      }
    });
  }

  getColaboradorDisplayName(item: Colaborador): string {
    return item ? `${item.nombre} ${item.apelllido}` : '';
  }

  // Para el grid: retorna el nombre completo del colaborador
  calculateFullName(data: any): string {
    const colab = this.colaboradores.find(c => c.colaboradorId === data.colaboradorId);
    return colab ? `${colab.nombre} ${colab.apelllido}` : data.colaboradorId;
  }

  // Para el grid: retorna el nombre de la sucursal
  getSucursalName(data: any): string {
    const suc = this.sucursales.find(s => s.sucursalId === data.sucursalId);
    return suc ? suc.nombre : data.sucursalId;
  }
}
