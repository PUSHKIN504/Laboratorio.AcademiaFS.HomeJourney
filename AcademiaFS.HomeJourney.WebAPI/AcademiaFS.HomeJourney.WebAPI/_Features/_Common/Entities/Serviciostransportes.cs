using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Serviciostransportes
{
    public int ServiciotransporteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Transportistas> Transportista { get; set; } = new List<Transportistas>();

    public virtual Usuarios UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuarios? UsuariomodificaNavigation { get; set; }
}
