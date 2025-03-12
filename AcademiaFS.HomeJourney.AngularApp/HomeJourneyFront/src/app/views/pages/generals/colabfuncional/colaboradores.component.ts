import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConfigurationComponent } from '../../../components/configuration.component';
import { Colaborador, CreatePersonaColaboradorDto } from '../../../models/colaborador.model';
import { ConfigurationBaseService } from '../../../services/configuration-base.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LeafletMouseEvent } from 'leaflet';
import {
  DxDataGridModule,
  DxFormModule,
  DxButtonModule,
  DxSelectBoxModule,
  DxNumberBoxModule,
  DxCheckBoxModule,
  DxTextBoxModule,
  DxPopupModule,
  DxScrollViewModule
} from 'devextreme-angular';
import { GoogleMapsModule } from '@angular/google-maps';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { CustomForm} from "../../../../shared/custom-popup/custom-popup.component"
import { ValidationPatterns } from '../../../../shared/validators/ValidationPatterns';
@Component({
  templateUrl: './colaboradores.component.html',
  styleUrls: ['./colaboradores.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    DxDataGridModule,
    DxFormModule,
    CustomForm,
    ReactiveFormsModule,
    DxPopupModule,
    DxScrollViewModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxNumberBoxModule,
    DxCheckBoxModule,
    DxTextBoxModule,
    MatToolbarModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    CommonModule
  ]
})
export class ColaboradoresComponent extends ConfigurationComponent<CreatePersonaColaboradorDto> implements OnInit {

  personas: Array<{ id: number; nombre: string }> = [
    { id: 1, nombre: 'Juan Pérez' },
    { id: 2, nombre: 'María Gómez' }
  ];
  roles: Array<{ rolId: number; nombre: string }> = [
    { rolId: 1, nombre: 'Administrador' },
    { rolId: 2, nombre: 'Colaborador' }
  ];
  ciudades: Array<{ ciudadId: number; nombre: string }> = [
    { ciudadId: 1, nombre: 'San Pedro Sula' },
    { ciudadId: 2, nombre: 'La Lima' }
  ];
  cargos: Array<{ cargoId: number; nombre: string }> = [
    { cargoId: 1, nombre: 'Gerente' },
    { cargoId: 2, nombre: 'Asistente' }
  ];
  estadociviles = [
    { estadocivilId: 1, nombre: 'Soltero' },
    { estadocivilId: 2, nombre: 'Casado' },
    // ... otros estados
  ];
  calculateFullName(data: any): string {
    return `${data.nombre} ${data.apellido}`;
  }
  constructor(snackBar: MatSnackBar) {
    super("academiafarsiman/personascolaboradores", "Colaborador", snackBar);
  }

  override onInitForm(): void {
    this._form = new FormGroup({
      colaboradorId: new FormControl(0),
      nombre: new FormControl(null, [Validators.required]),
      apelllido: new FormControl(null, [Validators.required]),
      sexo: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      documentonacionalidentificacion: new FormControl(null, [
        Validators.required,
        Validators.pattern(/^[0-9]+$/)
      ]),
      ciudadId: new FormControl(null, [Validators.required]),
      estadocivilId: new FormControl(null, [Validators.required]),
      usuariocrea: new FormControl(1, [Validators.required]),
      rolId: new FormControl(null, [Validators.required]),
      cargoId: new FormControl(null, [Validators.required]),
      direccion: new FormControl(null, [Validators.required, Validators.maxLength(500)]),
      activo: new FormControl(true, [Validators.required]),
      latitud: new FormControl(null, [Validators.required]),
      longitud: new FormControl(null, [Validators.required])
    });
  }
  
  onLocationSelected(event: LeafletMouseEvent): void {
    this._form.patchValue({
      latitud: event.latlng.lat,
      longitud: event.latlng.lng
    });
  }
  ngOnInit(): void {
    super.get(true);
  }
}
