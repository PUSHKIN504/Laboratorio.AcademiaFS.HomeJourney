// export interface ViajesdetallesCreateClusteredDto {
//     colaboradorId?: number;
//     distanciakilometros?: number;
//     totalpagar?: number;
//     colaboradorsucursalId?: number;
//     monedaId?: number;
//     latitud?: number;
//     longitud?: number;
//   }
  
//   export interface ViajesCreateClusteredDto {
//     sucursalId?: number;
//     transportistaIds: number[];
//     estadoId: number;
//     viajehora: string; // Ejemplo: "14:30"
//     viajefecha: Date;
//     usuariocrea: number;
//     monedaId?: number;
//   }
  
//   export interface CreateViajesRequest {
//     viajeclusteredDto: ViajesCreateClusteredDto;
//     empleadosclusteredDto: ViajesdetallesCreateClusteredDto[][];
//   }
  

  export interface ViajesCreateClusteredDto {
    sucursalId: number;
    transportistaIds: number[];
    estadoId: number;
    viajehora: string;     
    viajefecha: Date;
    usuariocrea: number;
    monedaId: number;
  }
  
  export interface ViajesdetallesCreateClusteredDto {
    colaboradorId: number;
    distanciakilometros: number;
    totalpagar: number;
    colaboradorsucursalId: number;
    monedaId: number;
    latitud: number;
    longitud: number;
  }
  
  export interface CreateViajesRequest {
    viajeclusteredDto: ViajesCreateClusteredDto;
    empleadosclusteredDto: ViajesdetallesCreateClusteredDto[][];
  }
  