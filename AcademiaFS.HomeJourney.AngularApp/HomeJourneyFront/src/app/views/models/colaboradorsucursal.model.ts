export interface ColaboradorSucursalDto {
    colaboradorSucursalId: number;
    colaboradorId: number;
    sucursalId: number;
    distanciaKilometro: number;
    activo: boolean;
    usuarioCrea: number;
    fechaCrea: Date;
    usuarioModifica?: number;
    fechaModifica?: Date;
  }
  