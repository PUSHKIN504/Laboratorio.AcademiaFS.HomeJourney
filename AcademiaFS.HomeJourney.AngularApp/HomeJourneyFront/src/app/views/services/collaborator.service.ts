import { Injectable } from '@angular/core';
import { ConfigurationBaseService } from './configuration-base.service';
import { ColaboradorSucursalDto } from '../../views/models/grals.model';
import { ColaboradorGetAllDto } from '../models/colaborador.model';

@Injectable({
  providedIn: 'root'
})
export class ColaboradorSucursalService extends ConfigurationBaseService<ColaboradorSucursalDto> {
  constructor() {
    super("academiafarsiman/colaboradoressucursales");
  }
}
@Injectable({
  providedIn: 'root'
})
export class ColaboradorService extends ConfigurationBaseService<ColaboradorGetAllDto> {
  constructor() {
    super("academiafarsiman/personascolaboradores");
  }
}

@Injectable({
  providedIn: 'root'
})
export class colaboradoressucursales extends ConfigurationBaseService<ColaboradorSucursalDto> {
  constructor() {
    super("academiafarsiman/colaboradoressucursales");
  }
}