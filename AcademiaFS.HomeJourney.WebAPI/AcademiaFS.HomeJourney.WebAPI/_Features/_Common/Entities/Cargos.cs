using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Cargo
{
    public int CargoId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Colaboradore> Colaboradores { get; set; } = new List<Colaboradore>();
}
