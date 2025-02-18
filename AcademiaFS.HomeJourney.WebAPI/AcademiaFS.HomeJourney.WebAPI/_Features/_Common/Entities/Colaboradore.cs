using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Colaboradore
{
    public int ColaboradorId { get; set; }

    public int PersonaId { get; set; }

    public int RolId { get; set; }

    public int CargoId { get; set; }

    public bool Activo { get; set; }

    public string Direccion { get; set; } = null!;

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public virtual Cargos Cargo { get; set; } = null!;

    public virtual ICollection<Colaboradoressucursale> Colaboradoressucursales { get; set; } = new List<Colaboradoressucursale>();

    public virtual Persona Persona { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;

    public virtual ICollection<Solicitudesviaje> Solicitudesviajes { get; set; } = new List<Solicitudesviaje>();

    public virtual Usuario? Usuario { get; set; }

    public virtual Usuario UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuario? UsuariomodificaNavigation { get; set; }

    public virtual ICollection<Valoracionesviaje> Valoracionesviajes { get; set; } = new List<Valoracionesviaje>();
}
