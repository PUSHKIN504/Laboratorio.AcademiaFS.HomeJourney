﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    [ExcludeFromCodeCoverage]
    public class Valoracionesviajes
    {
        public int ValoracionviajeId { get; set; }
        public byte Valoracionnota { get; set; }
        public int ColaboradorId { get; set; }
        public int ViajeId { get; set; }

        // Relaciones
        public Colaboradores Colaborador { get; set; } = null!;
        public Viajes Viaje { get; set; } = null!;
    }
}