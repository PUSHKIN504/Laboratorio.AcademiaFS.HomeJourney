import { Injectable } from '@angular/core';
import { ConfigurationBaseService } from './configuration-base.service';
import { SucursalDto } from '../models/grals.model';

@Injectable({
  providedIn: 'root'
})
export class SucursalService extends ConfigurationBaseService<SucursalDto> {
  constructor() {
    // Se utiliza el endpoint para obtener las sucursales.
    super("academiafarsiman/sucursales");
  }
}
