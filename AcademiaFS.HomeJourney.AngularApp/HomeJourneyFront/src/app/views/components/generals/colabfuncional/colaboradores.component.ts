import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConfigurationComponent } from '../../../components/configuration.component';
import { Colaborador } from '../../../models/colaborador.model';
import { ConfigurationBaseService } from '../../../services/configuration-base.service';
import { MatSnackBar } from '@angular/material/snack-bar';

import {
  DxDataGridModule,
  DxFormModule,
  DxButtonModule,
  DxSelectBoxModule,
  DxNumberBoxModule,
  DxCheckBoxModule,
  DxTextBoxModule
} from 'devextreme-angular';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  templateUrl: './colaboradores.component.html',
  styleUrls: ['./colaboradores.component.scss'],
  standalone: true,
  imports: [
    ReactiveFormsModule,
    DxDataGridModule,
    DxFormModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxNumberBoxModule,
    DxCheckBoxModule,
    DxTextBoxModule,
    MatToolbarModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule
  ]
})
export class ColaboradoresComponent extends ConfigurationComponent<Colaborador> implements OnInit {
  // Opciones para los dropdowns
  personas: Array<{ id: number; nombre: string }> = [
    { id: 1, nombre: 'Juan Pérez' },
    { id: 2, nombre: 'María Gómez' }
  ];
  roles: Array<{ id: number; nombre: string }> = [
    { id: 1, nombre: 'Administrador' },
    { id: 2, nombre: 'Colaborador' }
  ];
  cargos: Array<{ id: number; nombre: string }> = [
    { id: 1, nombre: 'Gerente' },
    { id: 2, nombre: 'Asistente' }
  ];

  constructor(snackBar: MatSnackBar) {
    // Pasa la URL, el texto y el snackBar al componente base
    super("configurations/colaboradores", "Colaborador", snackBar);
  }

  override onInitForm(): void {
    // Definimos los controles del formulario para Colaborador
    this._form = new FormGroup({
      colaboradorId: new FormControl<number | null>(0),
      personaId: new FormControl<number | null>(null, [Validators.required]),
      rolId: new FormControl<number | null>(null, [Validators.required]),
      cargoId: new FormControl<number | null>(null, [Validators.required]),
      activo: new FormControl<boolean | null>(true, [Validators.required]),
      direccion: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(500)])
      // Agrega otros campos (como latitud, longitud) si es necesario
    });
  }

  ngOnInit(): void {
    // Carga los colaboradores usando el método del componente base
    super.get(true);
  }
}
