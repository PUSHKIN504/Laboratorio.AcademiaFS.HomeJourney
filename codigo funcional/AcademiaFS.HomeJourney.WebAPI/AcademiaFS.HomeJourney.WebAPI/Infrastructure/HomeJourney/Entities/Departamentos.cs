using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Departamentos : IActivableInterface
    {
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Activo { get; set; }
        public int? PaisId { get; set; }

        // Relaciones
        public Paises? Pais { get; set; }
        public ICollection<Ciudades> Ciudades { get; set; } = new List<Ciudades>();
    }
}