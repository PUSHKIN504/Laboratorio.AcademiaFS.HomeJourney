using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Departamento
{
    public int DepartamentoId { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public int? PaisId { get; set; }

    public virtual ICollection<Ciudades> Ciudades { get; set; } = new List<Ciudades>();

    public virtual Paise? Pais { get; set; }
}
