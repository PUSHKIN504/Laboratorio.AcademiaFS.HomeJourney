import { Injectable } from '@angular/core';
import { ConfigurationBaseService } from './configuration-base.service';
import { ColaboradorSucursalDto } from '../../views/models/grals.model';

@Injectable({
  providedIn: 'root'
})
export class ColaboradorSucursalService extends ConfigurationBaseService<ColaboradorSucursalDto> {
  constructor() {
    // Se utiliza el endpoint de asignaciones de colaborador a sucursal.
    super("academiafarsiman/colaboradoressucursales");
  }
}
