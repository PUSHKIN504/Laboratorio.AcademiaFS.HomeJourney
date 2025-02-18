﻿using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Pantalla
{
    public int PantallaId { get; set; }

    public string Nombre { get; set; } = null!;

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Pantallasrole> Pantallasroles { get; set; } = new List<Pantallasrole>();

    public virtual Usuario UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuario? UsuariomodificaNavigation { get; set; }
}
