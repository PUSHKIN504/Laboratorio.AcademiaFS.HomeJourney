using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Transportistas
    {
        public int TransportistaId { get; set; }
        public int ServiciotransporteId { get; set; }
        public decimal Tarifaporkilometro { get; set; }
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int PersonaId { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public int? MonedaId { get; set; }

        // Relaciones
        public Serviciostransportes Serviciotransporte { get; set; } = null!;
        public Personas Persona { get; set; } = null!;
        public Monedas Moneda { get; set; }
        public ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();
    }
}