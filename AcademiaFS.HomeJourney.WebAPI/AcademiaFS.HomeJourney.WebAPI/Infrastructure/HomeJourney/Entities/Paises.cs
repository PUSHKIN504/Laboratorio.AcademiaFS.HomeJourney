using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Paises
    {
        public int PaisId { get; set; }
        public string? Nombre { get; set; }

        // Relaciones
        public ICollection<Departamentos> Departamentos { get; set; } = new List<Departamentos>();
    }
}