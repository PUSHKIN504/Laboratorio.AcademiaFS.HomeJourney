using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    [ExcludeFromCodeCoverage]
    public class Pantallas
    {
        public int PantallaId { get; set; }
        public string Nombre { get; set; } = null!;
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public bool Activo { get; set; }

        // Relaciones
        public ICollection<Pantallasroles> Pantallasroles { get; set; } = new List<Pantallasroles>();
    }
}