using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Estadosciviles
    {
        public int EstadocivilId { get; set; }
        public string Nombre { get; set; } = null!;

        // Relaciones
        public ICollection<Personas> Personas { get; set; } = new List<Personas>();
    }
}