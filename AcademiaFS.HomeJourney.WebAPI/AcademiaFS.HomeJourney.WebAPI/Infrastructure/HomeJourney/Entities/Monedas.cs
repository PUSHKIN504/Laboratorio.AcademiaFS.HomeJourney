using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Monedas
    {
        public int MonedaId { get; set; }
        public string? Nombre { get; set; }
        public string? Simbolo { get; set; }
        public decimal? ValorLempiras { get; set; }

        // Relaciones
        public ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();
        public ICollection<Viajesdetalles> Viajesdetalles { get; set; } = new List<Viajesdetalles>();
    }
}