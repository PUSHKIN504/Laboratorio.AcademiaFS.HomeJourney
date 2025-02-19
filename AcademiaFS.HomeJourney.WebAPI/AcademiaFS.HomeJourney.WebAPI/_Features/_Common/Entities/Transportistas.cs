using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Transportistas
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

    public virtual Personas Persona { get; set; } = null!;

    public virtual Serviciostransportes Serviciotransporte { get; set; } = null!;

    public virtual Usuarios UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuarios? UsuariomodificaNavigation { get; set; }

    public virtual ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();
}
