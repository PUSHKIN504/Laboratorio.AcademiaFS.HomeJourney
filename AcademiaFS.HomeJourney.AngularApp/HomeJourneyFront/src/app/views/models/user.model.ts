export interface UsuarioDto {
    usuarioId: number;
    username: string;
    colaboradorId: number;
    esadmin: boolean;
    activo: boolean;
  }
  
  export interface UsuarioLoginRequest {
    username: string;
    password: string;
  }
  
  export interface UsuarioConDetallesDto {
    usuarioId: number;
    username: string;
    personaNombreCompleto: string;
    cargo: string;
    rol: string;
    sucursalId?: number;
    sucursalNombre?: string;
  }