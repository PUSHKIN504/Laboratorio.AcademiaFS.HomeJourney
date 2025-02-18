using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Pantallasrole
{
    public int PantallarolId { get; set; }

    public int RolId { get; set; }

    public int PantallaId { get; set; }

    public DateTime Fechacrea { get; set; }

    public virtual Pantalla Pantalla { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;
}
