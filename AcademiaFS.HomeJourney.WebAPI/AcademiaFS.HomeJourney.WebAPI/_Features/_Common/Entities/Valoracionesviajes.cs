using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Valoracionesviajes
{
    public int ValoracionviajeId { get; set; }

    public byte Valoracionnota { get; set; }

    public int ColaboradorId { get; set; }

    public int ViajeId { get; set; }

    public virtual Colaboradorses Colaborador { get; set; } = null!;

    public virtual Viajes Viaje { get; set; } = null!;
}
