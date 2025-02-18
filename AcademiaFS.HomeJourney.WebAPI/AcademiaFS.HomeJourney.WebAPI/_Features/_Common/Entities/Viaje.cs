using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Viaje
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

    public virtual Estado Estado { get; set; } = null!;

    public virtual Moneda? Moneda { get; set; }

    public virtual ICollection<Solicitudesviaje> Solicitudesviajes { get; set; } = new List<Solicitudesviaje>();

    public virtual Sucursale Sucursal { get; set; } = null!;

    public virtual Transportista Transportista { get; set; } = null!;

    public virtual Usuario UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuario? UsuariomodificaNavigation { get; set; }

    public virtual ICollection<Valoracionesviaje> Valoracionesviajes { get; set; } = new List<Valoracionesviaje>();

    public virtual ICollection<Viajesdetalle> Viajesdetalles { get; set; } = new List<Viajesdetalle>();
}
