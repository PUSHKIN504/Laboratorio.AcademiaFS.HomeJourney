using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Monedas
{
    public int MonedaId { get; set; }

    public string? Nombre { get; set; }

    public string? Simbolo { get; set; }

    public decimal? ValorLempiras { get; set; }

    public virtual ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();

    public virtual ICollection<Viajesdetalles> Viajesdetalles { get; set; } = new List<Viajesdetalles>();
}
