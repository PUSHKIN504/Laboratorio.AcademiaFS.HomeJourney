import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConfigurationComponent } from '../../../components/configuration.component';
import { Colaborador, ColaboradorGetAllDto, CreatePersonaColaboradorDto } from '../../../models/colaborador.model';
import { ConfigurationBaseService } from '../../../services/configuration-base.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GoogleMapsModule } from '@angular/google-maps';
import {
  DxDataGridModule,
  DxFormModule,
  DxButtonModule,
  DxSelectBoxModule,
  DxNumberBoxModule,
  DxCheckBoxModule,
  DxTextBoxModule,
  DxPopupModule,
  DxScrollViewModule,
  DxDataGridComponent
} from 'devextreme-angular';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { CustomForm } from "../../../../shared/custom-popup/custom-popup.component";
import { ValidationPatterns } from '../../../../shared/validators/ValidationPatterns';
import { ColaboradorSucursalDto } from '../../../models/colaboradorsucursal.model';
import { SucursalService } from '../../../services/sucursal.service';
import { ColaboradorService } from '../../../services/collaborator.service';

@Component({
  templateUrl: './colaboradores-sucursales.component.html',
  styleUrls: ['./colaboradores-sucursales.component.scss'],
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
    CommonModule,
    GoogleMapsModule
  ]
})
export class ColaboradoresSucursalesComponent extends ConfigurationComponent<ColaboradorSucursalDto> implements OnInit {
  collaborators: Array<ColaboradorGetAllDto> = [];
  sucursales: Array<{ sucursalId: number, nombre: string }> = [];

  @ViewChild(DxDataGridComponent) dataGrid!: DxDataGridComponent;

  constructor(
    snackBar: MatSnackBar,
    private collaboratorService: ColaboradorService,
    private sucursalService: SucursalService
  ) {
    super("academiafarsiman/colaboradoressucursales", "Colaborador", snackBar);
  }

  ngOnInit(): void {
    super.get(true);
    this.loadCollaborators();
    this.loadSucursales();
  }

  loadCollaborators(): void {
    this.collaboratorService.get()
      .then(response => {
        const data = Array.isArray(response) ? response : response.data || [];
        this.collaborators = data.map(collab => ({
          ...collab,
          nombreCompleto: `${collab.nombre} ${collab.apellido}`
        }));
        if (this.dataGrid) {
          this.dataGrid.instance.refresh();
        }
      })
      .catch(error => {
        console.error("Error al obtener colaboradores:", error);
        this.collaborators = [];
      });
  }

  loadSucursales(): void {
    this.sucursalService.get()
      .then(response => {
        this.sucursales = response.data.map(suc => ({
          sucursalId: suc.sucursalId,
          nombre: suc.nombre
        }));
      })
      .catch(error => {
        console.error("Error al obtener sucursales:", error);
      });
  }

  override onInitForm(): void {
    this._form = new FormGroup({
      colaboradorSucursalId: new FormControl(0),
      colaboradorId: new FormControl(null, [Validators.required]),
      sucursalId: new FormControl(null, [Validators.required]),
      distanciaKilometro: new FormControl(0),
      activo: new FormControl(true, [Validators.required]),
      usuarioCrea: new FormControl(1),
      fechaCrea: new FormControl(new Date()),
      usuarioModifica: new FormControl(1),
      fechaModifica: new FormControl(null),
    });
  }

  // Map-related properties (unchanged from your original code)
  mapCenter: google.maps.LatLngLiteral = { lat: 15.5000, lng: -88.0333 };
  mapZoom: number = 12;
  markerPosition?: google.maps.LatLngLiteral;

  calculateFullName(data: any): string {
    const nombre = JSON.stringify(data.nombre);
    const apellido = JSON.stringify(data.apellido);
    return `${nombre.slice(1, -1)} ${apellido.slice(1, -1)}`;
  }
}