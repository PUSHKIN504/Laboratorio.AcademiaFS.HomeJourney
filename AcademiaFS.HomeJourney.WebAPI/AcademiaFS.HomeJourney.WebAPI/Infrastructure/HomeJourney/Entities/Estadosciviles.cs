using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Estadosciviles : IActivableInterface
    {
        public int EstadocivilId { get; set; }
        public string Nombre { get; set; } = null!;
        [NotMapped]
        public bool Activo { get; set; }
        // Relaciones
        public ICollection<Personas> Personas { get; set; } = new List<Personas>();
    }
}