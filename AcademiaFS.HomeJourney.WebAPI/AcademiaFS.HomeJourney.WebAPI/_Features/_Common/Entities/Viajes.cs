using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Viajes
{
    public int ViajeId { get; set; }

    public int SucursalId { get; set; }

    public int TransportistaId { get; set; }

    public int EstadoId { get; set; }

    public TimeOnly Viajehora { get; set; }

    public DateOnly Viajefecha { get; set; }

    public decimal Totalkilometros { get; set; }

    public decimal Totalpagar { get; set; }

    public bool Activo { get; set; }

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public int? MonedaId { get; set; }

    public virtual Estados Estado { get; set; } = null!;

    public virtual Monedas? Moneda { get; set; }

    public virtual ICollection<Solicitudesviajes> Solicitudesviajes { get; set; } = new List<Solicitudesviajes>();

    public virtual Sucursales Sucursal { get; set; } = null!;

    public virtual Transportistas Transportista { get; set; } = null!;

    public virtual Usuarios UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuarios? UsuariomodificaNavigation { get; set; }

    public virtual ICollection<Valoracionesviajes> Valoracionesviajes { get; set; } = new List<Valoracionesviajes>();

    public virtual ICollection<Viajesdetalles> Viajesdetalles { get; set; } = new List<Viajesdetalles>();
}
