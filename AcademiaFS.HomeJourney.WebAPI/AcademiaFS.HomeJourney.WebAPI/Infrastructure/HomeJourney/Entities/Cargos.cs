using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities {
    public class Cargos
    {
        public int CargoId { get; set; }
        public string? Nombre { get; set; }

        public ICollection<Colaboradores> Colaboradores { get; set; } = new List<Colaboradores>();
    }
}

