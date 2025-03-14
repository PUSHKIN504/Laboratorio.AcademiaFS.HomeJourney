export interface Transportistas {
    transportistaId: number;
    servicioTransporteId: number;
    tarifaPorKilometro: number;
    activo: boolean;
    usuarioCrea: number;
    fechaCrea: Date;
    personaId: number;
    usuarioModifica?: number | null;
    fechaModifica?: Date | null;
    monedaId?: number | null;
  }