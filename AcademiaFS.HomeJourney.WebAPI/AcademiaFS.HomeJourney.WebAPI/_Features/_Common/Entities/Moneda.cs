using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Moneda
{
    public int MonedaId { get; set; }

    public string? Nombre { get; set; }

    public string? Simbolo { get; set; }

    public decimal? ValorLempiras { get; set; }

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();

    public virtual ICollection<Viajesdetalle> Viajesdetalles { get; set; } = new List<Viajesdetalle>();
}
