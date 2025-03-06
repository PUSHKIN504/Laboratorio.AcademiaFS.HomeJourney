using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    [ExcludeFromCodeCoverage]
    public class Estados : IActivableInterface
    {
        public int EstadoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        [NotMapped]
        public bool Activo { get; set; }
        // Relaciones
        public ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();
        public ICollection<Solicitudesviajes> Solicitudesviajes { get; set; } = new List<Solicitudesviajes>();
    }
}