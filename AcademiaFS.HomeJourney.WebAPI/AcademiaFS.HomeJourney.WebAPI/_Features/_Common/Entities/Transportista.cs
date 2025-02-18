using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Transportista
{
    public int TransportistaId { get; set; }

    public int ServiciotransporteId { get; set; }

    public decimal Tarifaporkilometro { get; set; }

    public bool Activo { get; set; }

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int PersonaId { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public int? MonedaId { get; set; }

    public virtual Persona Persona { get; set; } = null!;

    public virtual Serviciostransporte Serviciotransporte { get; set; } = null!;

    public virtual Usuario UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuario? UsuariomodificaNavigation { get; set; }

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
