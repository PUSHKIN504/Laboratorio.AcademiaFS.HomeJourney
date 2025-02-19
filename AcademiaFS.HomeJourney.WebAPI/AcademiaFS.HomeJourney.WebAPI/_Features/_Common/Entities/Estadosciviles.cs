using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Estadosciviles
{
    public int EstadocivilId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Personas> Personas { get; set; } = new List<Personas>();
}
