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

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { CustomForm} from "../../../../shared/custom-popup/custom-popup.component"
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
  calculateFullName(data: any): string {
    return `${data.nombre} ${data.apellido}`;
  }
  constructor(snackBar: MatSnackBar) {
    super("academiafarsiman/personascolaboradores", "Colaborador", snackBar);
  }

  override onInitForm(): void {
    this._form = new FormGroup({
      usuariocrea: new FormControl<number | null>(1, [Validators.required]),
      colaboradorId: new FormControl<number | null>(0),
      personaId: new FormControl<number | null>(null, [Validators.required]),
      rolId: new FormControl<number | null>(null, [Validators.required]),
      cargoId: new FormControl<number | null>(null, [Validators.required]),
      activo: new FormControl<boolean | null>(true, [Validators.required]),
      direccion: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(500)])
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
