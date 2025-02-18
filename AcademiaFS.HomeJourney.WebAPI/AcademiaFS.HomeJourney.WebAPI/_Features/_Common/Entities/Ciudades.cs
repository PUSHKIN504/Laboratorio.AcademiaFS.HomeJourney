using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Ciudades
{
    public int CiudadId { get; set; }

    public string Nombre { get; set; } = null!;

    public int DepartamentoId { get; set; }

    public bool Activo { get; set; }

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
