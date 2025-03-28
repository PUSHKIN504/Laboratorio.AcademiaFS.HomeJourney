﻿using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto
{
    [ExcludeFromCodeCoverage]
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; } = null!;
        public int ColaboradorId { get; set; }
        public bool Esadmin { get; set; }
        public bool Activo { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class UsuarioLoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    [ExcludeFromCodeCoverage]
    public class UsuarioConDetallesDto
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; } = null!;
        public string PersonaNombreCompleto { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public int? SucursalId { get; set; }
        public string? SucursalNombre { get; set; }
    }

}
