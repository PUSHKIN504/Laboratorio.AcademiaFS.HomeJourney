import { Component, OnInit, ViewChild, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DxDataGridComponent, DxDataGridModule, DxButtonModule, DxSelectBoxModule, DxTextBoxModule, DxDateBoxModule } from 'devextreme-angular';
import { MatTabsModule } from '@angular/material/tabs';
import Swal from 'sweetalert2';
import { ViajesService } from '../../../services/viaje.service';
import { ViajesdetallesCreateClusteredDto, ViajesCreateClusteredDto, CreateViajesRequest } from '../../../models/viaje.model';
import { SucursalService } from '../../../services/sucursal.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { colaboradoressucursales, transportistas } from '../../../services/collaborator.service';
import { ColaboradorSucursalDto } from '../../../models/colaboradorsucursal.model';
import { SucursalDto } from '../../../models/grals.model';
import { Transportistas } from '../../../models/Transportista.model';
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
  collaborators: ColaboradorSucursalDto[] = [];
  selectedCollaborators: any[] = [];
  transportistas: Transportistas[] = [];
  selectedTransportistas: any[] = [];
  sucursales: SucursalDto[] = [];
  viajeForm!: FormGroup;
  timeOptions: string[] = [];
  clusteredEmployees: ViajesdetallesCreateClusteredDto[] = [];

  @ViewChild(DxDataGridComponent) dataGrid!: DxDataGridComponent;

  constructor(
    private snackBar: MatSnackBar,
    private viajesService: ViajesService,
    private sucursalService: SucursalService,
    private collaboratorService: colaboradoressucursales,
    private transportistasService: transportistas
  ) {}

  ngOnInit(): void {
    this.loadCollaborators();
    this.loadTransportistas();
    this.initForm();
    this.loadSucursales();
    this.generateTimeArray();
    this.timeOptions = this.generateTimeArray();
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      const user = JSON.parse(storedUser);
      console.log('Datos del usuario:', user);
    } else {
      console.log('No hay datos de usuario almacenados');
    }

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
    this.collaboratorService.get()
      .then(response => {
        this.collaborators = response.data.map((collab: any) => ({
          ...collab
        }));
      })
      .catch(error => {
        console.error("Error al obtener colaboradores:", error);
        Swal.fire({
          title: 'Error',
          text: 'Error al obtener colaboradores',
          icon: 'error',
          confirmButtonText: 'Cerrar'
        });
      });
  }

  loadTransportistas(): void {
    this.transportistasService.get()
      .then(response => {
        this.transportistas = response.data.map((trans: any) => ({
          ...trans
        }));
      })
      .catch(error => {
        console.error("Error al obtener transportistas:", error);
        Swal.fire({
          title: 'Error',
          text: 'Error al obtener transportistas',
          icon: 'error',
          confirmButtonText: 'Cerrar'
        });
      });
  }
   generateTimeArray(): string[] {
    const times: string[] = [];
    for (let hour = 0; hour < 24; hour++) {
      for (let minute of [0, 30]) {
        const hStr = hour.toString().padStart(2, '0');
        const mStr = minute.toString().padStart(2, '0');
        times.push(`${hStr}:${mStr}:00`);
      }
    }
    return times;
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

    const employeesPayload: ColaboradorSucursalDto[] = this.selectedCollaborators.map(collab => ({
      colaboradorId: collab.colaboradorId,
      distanciakilometros: collab.distanciaKilometro, 
      totalpagar: 0,
      colaboradorsucursalId: collab.colaboradorsucursalId,
      monedaId: collab.monedaId,
      latitud: collab.latitud,
      longitud: collab.longitud
    }));
      
    
    const usuarioCrea = 1;
    const distanceThreshold = 50; 

    this.viajesService.clusterEmployees(usuarioCrea, employeesPayload, distanceThreshold)
      .then((response:any) => {
        this.clusteredEmployees = response[0];
        console.log(this.clusteredEmployees);
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
        icon: 'warning',
        confirmButtonText: 'Cerrar'
      });
      return;
    }
    if (this.viajeForm.invalid) {
      this.viajeForm.markAllAsTouched();
      Swal.fire({
        title: 'Error',
        text: 'Complete los campos requeridos para el viaje',
        icon: 'warning',
        confirmButtonText: 'Cerrar'
      });
      return;
    }
  
    const usuarioCrea = 1;
  
    const viajeClusteredDto: ViajesCreateClusteredDto = {
      sucursalId: this.viajeForm.value.sucursalId,
      estadoId: 4,
      viajehora: this.viajeForm.value.viajehora, 
      viajefecha: this.viajeForm.value.viajefecha,
      usuariocrea: usuarioCrea,
      monedaId: this.viajeForm.value.monedaId || 1,
      transportistaIds: this.selectedTransportistas.map((t: any) => t.transportistaId)
    };
  
    const employeesPayload: ViajesdetallesCreateClusteredDto[] = this.clusteredEmployees.map(collab => ({
      colaboradorId: collab.colaboradorId,
      distanciakilometros: collab.distanciakilometros || 0,
      totalpagar: 0,
      colaboradorsucursalId: collab.colaboradorsucursalId,
      monedaId: collab.monedaId|| 1,
      latitud: collab.latitud,
      longitud: collab.longitud
    }));
  
    const empleadosclusteredDto: ViajesdetallesCreateClusteredDto[][] = [employeesPayload];
  
    const request: CreateViajesRequest = {
      viajeclusteredDto: viajeClusteredDto,
      empleadosclusteredDto: empleadosclusteredDto
    };
  
    this.viajesService.createTripsFromClusters(usuarioCrea, request)
      .then((response: any) => {
        Swal.fire({
          title: 'Éxito',
          text: 'Viaje(s) creado(s) correctamente',
          icon: 'success',
          confirmButtonText: 'Cerrar'
        });
      })
      .catch((error: any) => {
        let errorMsg = 'Error al crear viajes';
        if (error.error && error.error.errors) {
          errorMsg = Object.values(error.error.errors).join(' ');
        } else if (error.error && error.error.Message) {
          errorMsg = error.error.Message;
        }
        Swal.fire({
          title: 'Error',
          text: error.error,
          icon: 'error',
          confirmButtonText: 'Cerrar'
        });
      });
  }
  
  
  loadSucursales(): void {
    this.sucursalService.get()
      .then((response:any) => {
        this.sucursales = response.data.map((suc: any) => ({
          ...suc
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
