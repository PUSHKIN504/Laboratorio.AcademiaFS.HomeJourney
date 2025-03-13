export interface Colaborador {
    colaboradorId: number;
    personaId: number;
    rolId: number;
    cargoId: number;
    activo: boolean;
    direccion: string;
    usuarioCrea: number;
    fechaCrea: Date;
    usuarioModifica?: number;
    fechaModifica?: Date;
    latitud: number;
    longitud: number;
  }
  export interface ColaboradorDto {
    colaboradorId: number;
    personaId: number;
    rolId: number;
    cargoId: number;
    activo: boolean;
    direccion: string;
    usuarioCrea: number;
    fechaCrea: Date;
    usuarioModifica?: number;
    fechaModifica?: Date;
    latitud: number;
    longitud: number;
  }
  
  export interface ColaboradorGetAllDto {
    colaboradorId?: number;
    personaId?: number;
    nombre?: string; // Desde Personas
    apellido?: string; // Desde Personas
    rolId?: number;
    cargoId?: number;
    activo?: boolean;
    direccion?: string;
    usuarioCrea?: number;
    fechaCrea?: Date;
    usuarioModifica?: number;
    fechaModifica?: Date;
    latitud?: number;
    longitud?: number;
    nombreCompleto?: string;
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
  
  export interface CustomResponse<T> {
    success: boolean;
    message: string;
    data: T;
  }
  