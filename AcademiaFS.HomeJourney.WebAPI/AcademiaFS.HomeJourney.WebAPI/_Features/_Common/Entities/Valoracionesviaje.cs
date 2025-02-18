using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Valoracionesviaje
{
    public int ValoracionviajeId { get; set; }

    public byte Valoracionnota { get; set; }

    public int ColaboradorId { get; set; }

    public int ViajeId { get; set; }

    public virtual Colaboradore Colaborador { get; set; } = null!;

    public virtual Viaje Viaje { get; set; } = null!;
}
