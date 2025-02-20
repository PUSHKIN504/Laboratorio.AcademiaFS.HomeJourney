using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Paises: IActivableInterface
    {
        public int PaisId { get; set; }
        public string? Nombre { get; set; }
        public bool Activo { get; set; }
        // Relaciones
        public ICollection<Departamentos> Departamentos { get; set; } = new List<Departamentos>();
    }
}