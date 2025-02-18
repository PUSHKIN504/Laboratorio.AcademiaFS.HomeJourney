using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Estado
{
    public int EstadoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Solicitudesviaje> Solicitudesviajes { get; set; } = new List<Solicitudesviaje>();

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
