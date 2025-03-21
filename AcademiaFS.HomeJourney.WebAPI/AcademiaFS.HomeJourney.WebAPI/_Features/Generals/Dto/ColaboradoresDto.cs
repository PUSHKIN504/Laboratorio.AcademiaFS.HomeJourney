﻿using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    [ExcludeFromCodeCoverage]
    public class ColaboradorDto
    {
        public int ColaboradorId { get; set; }
        public int PersonaId { get; set; }
        public int RolId { get; set; }
        public int CargoId { get; set; }
        public bool Activo { get; set; }
        public string Direccion { get; set; } = null!;
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class ColaboradorGetAllDto
    {
        public int ColaboradorId { get; set; }
        public int PersonaId { get; set; }
        public string Nombre { get; set; } // Desde Personas
        public string Apellido { get; set; } // Desde Personas
        public int RolId { get; set; }
        public int CargoId { get; set; }
        public bool Activo { get; set; }
        public string Direccion { get; set; } = null!;
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }
}
