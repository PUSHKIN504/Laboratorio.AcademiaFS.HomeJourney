using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    [ExcludeFromCodeCoverage]
    public class Paises: IActivableInterface
    {
        public int PaisId { get; set; }
        public string? Nombre { get; set; }
        public bool Activo { get; set; }
        public ICollection<Departamentos> Departamentos { get; set; } = new List<Departamentos>();
    }
}