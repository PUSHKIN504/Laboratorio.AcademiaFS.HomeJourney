using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Viajesdetalle
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

    public virtual Colaboradoressucursale Colaboradorsucursal { get; set; } = null!;

    public virtual Moneda? Moneda { get; set; }

    public virtual Usuario UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuario? UsuariomodificaNavigation { get; set; }

    public virtual Viaje Viaje { get; set; } = null!;
}
