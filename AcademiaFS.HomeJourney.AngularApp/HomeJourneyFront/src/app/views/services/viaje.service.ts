import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { 
  ViajesdetallesCreateClusteredDto, 
  ViajesCreateClusteredDto, 
  CreateViajesRequest 
} from '../models/viaje.model';
import { ColaboradorSucursalDto } from '../models/colaboradorsucursal.model';

@Injectable({
  providedIn: 'root'
})
export class ViajesService {
  // URL base del API para viajes clustered
  private readonly apiUrl = 'api/viajesclustered';

  constructor(private http: HttpClient) { }

 
  clusterEmployees(
    usuarioCrea: number, 
    employees: ColaboradorSucursalDto[], 
    distanceThreshold: number
  ): Promise<ViajesdetallesCreateClusteredDto[]> {
    const params = new HttpParams()
      .set('usuarioCrea', usuarioCrea.toString())
      .set('distanceThreshold', distanceThreshold.toString());

    return firstValueFrom(
      this.http.post<ViajesdetallesCreateClusteredDto[]>(`http://localhost:36955/api/viajesclustered/cluster-employees`, employees, { params })
    );
  }

 
  createTripsFromClusters(
    usuarioCrea: number, 
    request: CreateViajesRequest
  ): Promise<any> {
    const params = new HttpParams().set('usuarioCrea', usuarioCrea.toString());

    return firstValueFrom(
      this.http.post<any>(`http://localhost:36955/api/viajesclustered/create-trips-from-clusters`, request, { params })
    );
  }
}
