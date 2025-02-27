using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities {
    public class Cargos : IActivableInterface
    {
        public int CargoId { get; set; }
        public string? Nombre { get; set; }
        [NotMapped]
        public bool Activo { get; set; }

        public ICollection<Colaboradores> Colaboradores { get; set; } = new List<Colaboradores>();
    }
}

