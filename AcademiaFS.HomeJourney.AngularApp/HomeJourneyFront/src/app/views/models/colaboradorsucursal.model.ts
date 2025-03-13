export interface ColaboradorSucursalDto {
    colaboradorsucursalId: number;
    colaboradorId: number;
    sucursalId: number;
    distanciaKilometro: number;
    activo: boolean;
    usuarioCrea: number;
    fechaCrea: Date;
    usuarioModifica?: number;
    fechaModifica?: Date;
    nombreColaborador?:string;
    nombreSucursal?:string;
  }
  