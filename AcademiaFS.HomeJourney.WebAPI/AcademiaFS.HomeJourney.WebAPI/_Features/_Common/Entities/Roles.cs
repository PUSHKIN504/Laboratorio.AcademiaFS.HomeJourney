using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Roles
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Colaboradorses> Colaboradores { get; set; } = new List<Colaboradorses>();

    public virtual ICollection<Pantallasroles> Pantallasroles { get; set; } = new List<Pantallasroles>();

    public virtual Usuarios UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuarios? UsuariomodificaNavigation { get; set; }
}
