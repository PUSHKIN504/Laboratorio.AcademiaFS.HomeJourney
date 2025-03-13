import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { 
  ViajesdetallesCreateClusteredDto, 
  ViajesCreateClusteredDto, 
  CreateViajesRequest 
} from '../models/viaje.model'; // Ajusta la ruta según tu proyecto

@Injectable({
  providedIn: 'root'
})
export class ViajesService {
  // URL base del API para viajes clustered
  private readonly apiUrl = 'api/viajesclustered';

  constructor(private http: HttpClient) { }

  /**
   * Llama al endpoint cluster-employees para agrupar empleados.
   * @param usuarioCrea El id del usuario que crea.
   * @param employees Array de empleados a agrupar.
   * @param distanceThreshold Límite de distancia.
   * @returns Promise con el array de ViajesdetallesCreateClusteredDto
   */
  clusterEmployees(
    usuarioCrea: number, 
    employees: ViajesdetallesCreateClusteredDto[], 
    distanceThreshold: number
  ): Promise<ViajesdetallesCreateClusteredDto[]> {
    const params = new HttpParams()
      .set('usuarioCrea', usuarioCrea.toString())
      .set('distanceThreshold', distanceThreshold.toString());

    return firstValueFrom(
      this.http.post<ViajesdetallesCreateClusteredDto[]>(`http://localhost:36955/api/viajesclustered/cluster-employees`, employees, { params })
    );
  }

  /**
   * Llama al endpoint create-trips-from-clusters para crear viajes a partir de clusters.
   * @param usuarioCrea El id del usuario que crea.
   * @param request Objeto CreateViajesRequest con los datos del viaje y empleados agrupados.
   * @returns Promise con la respuesta del endpoint.
   */
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
