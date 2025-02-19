using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Estados
    {
        public int EstadoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        // Relaciones
        public ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();
        public ICollection<Solicitudesviajes> Solicitudesviajes { get; set; } = new List<Solicitudesviajes>();
    }
}