using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Paises
{
    public int PaisId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Departamentos> Departamentos { get; set; } = new List<Departamentos>();
}
