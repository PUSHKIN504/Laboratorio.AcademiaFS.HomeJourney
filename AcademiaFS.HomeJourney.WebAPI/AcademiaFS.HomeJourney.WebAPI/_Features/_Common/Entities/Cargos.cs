using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Cargos
{
    public int CargoId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Colaboradorses> Colaboradores { get; set; } = new List<Colaboradorses>();
}
