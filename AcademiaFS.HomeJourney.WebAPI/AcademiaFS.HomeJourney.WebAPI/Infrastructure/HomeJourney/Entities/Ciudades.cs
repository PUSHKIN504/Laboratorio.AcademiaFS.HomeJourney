using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities

{
    public class Ciudades
    {
        public int CiudadId { get; set; }
        public string Nombre { get; set; } = null!;
        public int DepartamentoId { get; set; }
        public bool Activo { get; set; }

        // Navegación: Cada ciudad pertenece a un departamento.
        public Departamentos Departamento { get; set; } = null!;
        // Relación: Una ciudad tiene muchas personas.
        public ICollection<Personas> Personas { get; set; } = new List<Personas>();
    }
}
