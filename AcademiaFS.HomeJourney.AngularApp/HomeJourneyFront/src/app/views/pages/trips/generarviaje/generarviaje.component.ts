import { Component, OnInit, ViewChild, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DxDataGridComponent, DxDataGridModule, DxButtonModule, DxSelectBoxModule, DxTextBoxModule, DxDateBoxModule } from 'devextreme-angular';
import { MatTabsModule } from '@angular/material/tabs';
import Swal from 'sweetalert2';
import { ViajesService } from '../../../services/viaje.service';
// Importa los DTO según tu proyecto (ajusta las rutas)
import { ViajesdetallesCreateClusteredDto, ViajesCreateClusteredDto, CreateViajesRequest } from '../../../models/viaje.model';
import { SucursalService } from '../../../services/sucursal.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
@Component({
  selector: 'app-viajes-clustered',
  templateUrl: './generarviaje.component.html',
  styleUrls: ['./generarviaje.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [ReactiveFormsModule, DxDataGridModule, DxButtonModule, DxSelectBoxModule, DxTextBoxModule, DxDateBoxModule, MatTabsModule,
    MatToolbarModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
  ]
})
export class ViajesClusteredComponent implements OnInit {
  currentTab: number = 0;
  collaborators: any[] = [];
  selectedCollaborators: any[] = [];
  transportistas: any[] = [];
  selectedTransportistas: any[] = [];
  sucursales: any[] = [{sucursalId: 1, nombre: 'centro'}];
  viajeForm!: FormGroup;

  clusteredEmployees: ViajesdetallesCreateClusteredDto[] = [];

  @ViewChild(DxDataGridComponent) dataGrid!: DxDataGridComponent;

  constructor(
    private snackBar: MatSnackBar,
    private viajesService: ViajesService,
    private sucursalService: SucursalService
  ) {}

  ngOnInit(): void {
    this.loadCollaborators();
    this.loadTransportistas();
    this.initForm();
  }

  initForm(): void {
    this.viajeForm = new FormGroup({
      sucursalId: new FormControl(null, Validators.required),
      estadoId: new FormControl(1, Validators.required),
      viajehora: new FormControl(null, Validators.required),
      viajefecha: new FormControl(new Date(), Validators.required),
      monedaId: new FormControl(null)
    });
  }

  loadCollaborators(): void {
    this.collaborators = [
      { colaboradorId: 1, nombre: 'Juan', apellido: 'Perez', nombreCompleto: 'Juan Perez', colaboradorsucursalId: 10, distanciaKilometro: 12, latitud: 15.5, longitud: -88.0 },
      { colaboradorId: 2, nombre: 'Maria', apellido: 'Gomez', nombreCompleto: 'Maria Gomez', colaboradorsucursalId: 11, distanciaKilometro: 8, latitud: 15.6, longitud: -88.1 }
    ];
  }

  loadTransportistas(): void {
    this.transportistas = [
      { transportistaId: 1, nombre: 'Transportista A' },
      { transportistaId: 2, nombre: 'Transportista B' },
      { transportistaId: 3, nombre: 'Transportista C' }
    ];
  }

  onNextFromCollaborators(): void {
    if (!this.selectedCollaborators || this.selectedCollaborators.length === 0) {
      Swal.fire({
        title: 'Error',
        text: 'Debe seleccionar al menos un colaborador',
        icon: 'warning'
      });
      return;
    }

    const employeesPayload: ViajesdetallesCreateClusteredDto[] = this.selectedCollaborators.map(collab => ({
      colaboradorId: collab.colaboradorId,
      distanciakilometros: collab.distanciaKilometro, 
      totalpagar: 0,
      colaboradorsucursalId: collab.colaboradorsucursalId,
      monedaId: undefined,
      latitud: collab.latitud,
      longitud: collab.longitud
    }));
      
    
    const usuarioCrea = 2;
    const distanceThreshold = 50; 

    this.viajesService.clusterEmployees(usuarioCrea, employeesPayload, distanceThreshold)
      .then((response:any) => {
        this.clusteredEmployees = response;
        this.currentTab = 1;
      })
      .catch((error:any) => {
        let errorMsg = 'Error al agrupar colaboradores';
        if (error.error && error.error.errors) {
          errorMsg = Object.values(error.error.errors).join(' ');
        }
        Swal.fire({
          title: 'Error',
          text: error.error || errorMsg,
          icon: 'error'
        });
      });
  }

  onNextFromTransportistas(): void {
    if (!this.selectedTransportistas || this.selectedTransportistas.length === 0) {
      Swal.fire({
        title: 'Error',
        text: 'Debe seleccionar al menos un transportista',
        icon: 'warning'
      });
      return;
    }
    if (this.viajeForm.invalid) {
      this.viajeForm.markAllAsTouched();
      Swal.fire({
        title: 'Error',
        text: 'Complete los campos requeridos para el viaje',
        icon: 'warning'
      });
      return;
    }

    const usuarioCrea = 1; 

    const viajeClusteredDto: ViajesCreateClusteredDto = {
      sucursalId: this.viajeForm.value.sucursalId,
      estadoId: this.viajeForm.value.estadoId,
      viajehora: this.viajeForm.value.viajehora,
      viajefecha: this.viajeForm.value.viajefecha,
      usuariocrea: usuarioCrea,
      monedaId: this.viajeForm.value.monedaId,
      transportistaIds: this.selectedTransportistas.map((t: any) => t.transportistaId)
    };    

    const empleadosclusteredDto = [this.clusteredEmployees];

    const request: CreateViajesRequest = {
      viajeclusteredDto: viajeClusteredDto,
      empleadosclusteredDto: empleadosclusteredDto
    };

    this.viajesService.createTripsFromClusters(usuarioCrea, request)
      .then((response: any) => {
        Swal.fire({
          title: 'Éxito',
          text: 'Viaje(s) creado(s) correctamente',
          icon: 'success'
        });
      })
      .catch((error: any) => {
        let errorMsg = 'Error al crear viajes';
        if (error.error && error.error.errors) {
          errorMsg = Object.values(error.error.errors).join(' ');
        }
        Swal.fire({
          title: 'Error',
          text: errorMsg,
          icon: 'error'
        });
      });
  }
  loadSucursales(): void {
    this.sucursalService.get()
      .then((response:any) => {
        this.sucursales = response.data.map((suc: any) => ({
          sucursalId: suc.sucursalId,
          nombre: suc.nombre
        }));
      })
      .catch((error:any) => {
        console.error('Error al cargar sucursales:', error);
        this.sucursales = [];
      });
  }
  onBack(): void {
    if (this.currentTab > 0) {
      this.currentTab--;
    }
  }
}
