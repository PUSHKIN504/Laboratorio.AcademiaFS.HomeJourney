export interface PersonaDto {
    personaId: number;
    nombre: string;
    apelllido: string;
    sexo: string;
    email: string;
    documentonacionalidentificacion: string;
    activo: boolean;
    estadocivilId?: number;
    ciudadId: number;
    usuariocrea: number;
    fechacrea: Date;
    usuariomodifica?: number;
    fechamodifica?: Date;
  }
  
  export interface CreatePersonaColaboradorDto {
    nombre: string;
    apelllido: string;
    sexo: string;
    email: string;
    documentonacionalidentificacion: string;
    activo: boolean;
    estadocivilId?: number;
    ciudadId: number;
    usuariocrea: number;
    rolId: number;
    cargoId: number;
    direccion: string;
    latitud: number;
    longitud: number;
  }
  export interface SucursalDto {
    sucursalId: number;
    nombre: string;
    direccion: string;
    activo: boolean;
    usuariocrea: number;
    fechacrea: Date;
    usuariomodifica?: number;
    fechamodifica?: Date;
    latitud: number;
    longitud: number;
    jefeId?: number;
  }
  export interface ColaboradorSucursalDto {
    colaboradorSucursalId: number;
    colaboradorId: number;
    sucursalId: number;
    distanciaKilometro: number;
    activo: boolean;
    usuarioCrea: number;
    fechacrea: Date;
    usuarioModifica?: number;
    fechamodifica?: Date;
  }
  export interface SucursalDto {
    sucursalId: number;
    nombre: string;
    direccion: string;
    activo: boolean;
    usuariocrea: number;
    fechacrea: Date;
    usuarioModifica?: number;
    fechamodifica?: Date;
    latitud: number;
    longitud: number;
    jefeId?: number;
  }
  