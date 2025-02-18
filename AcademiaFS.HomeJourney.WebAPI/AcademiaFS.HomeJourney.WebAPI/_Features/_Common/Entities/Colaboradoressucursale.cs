using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Colaboradoressucursale
{
    public int ColaboradorsucursalId { get; set; }

    public int ColaboradorId { get; set; }

    public int SucursalId { get; set; }

    public decimal Distanciakilometro { get; set; }

    public bool Activo { get; set; }

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public virtual Colaboradore Colaborador { get; set; } = null!;

    public virtual Sucursale Sucursal { get; set; } = null!;

    public virtual Usuario UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuario? UsuariomodificaNavigation { get; set; }

    public virtual ICollection<Viajesdetalle> Viajesdetalles { get; set; } = new List<Viajesdetalle>();
}
