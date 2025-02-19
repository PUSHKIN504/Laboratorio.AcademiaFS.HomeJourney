using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Colaboradorses
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

    public virtual ICollection<Colaboradoressucursales> Colaboradoressucursales { get; set; } = new List<Colaboradoressucursales>();

    public virtual Personas Persona { get; set; } = null!;

    public virtual Roles Rol { get; set; } = null!;

    public virtual ICollection<Solicitudesviajes> Solicitudesviajes { get; set; } = new List<Solicitudesviajes>();

    public virtual Usuarios? Usuario { get; set; }

    public virtual Usuarios UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuarios? UsuariomodificaNavigation { get; set; }

    public virtual ICollection<Valoracionesviajes> Valoracionesviajes { get; set; } = new List<Valoracionesviajes>();
}
