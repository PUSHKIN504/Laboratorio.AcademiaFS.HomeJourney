using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Viajesdetalles
{
    public int ViajedetalleId { get; set; }

    public int ViajeId { get; set; }

    public int ColaboradorId { get; set; }

    public decimal Distanciakilometros { get; set; }

    public decimal Totalpagar { get; set; }

    public int ColaboradorsucursalId { get; set; }

    public bool Activo { get; set; }

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public int? MonedaId { get; set; }

    public virtual Colaboradoressucursales Colaboradorsucursal { get; set; } = null!;

    public virtual Monedas? Moneda { get; set; }

    public virtual Usuarios UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuarios? UsuariomodificaNavigation { get; set; }

    public virtual Viajes Viaje { get; set; } = null!;
}
