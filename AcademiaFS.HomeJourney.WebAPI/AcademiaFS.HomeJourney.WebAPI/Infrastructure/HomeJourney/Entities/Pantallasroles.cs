using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Pantallasroles
    {
        public int PantallarolId { get; set; }
        public int RolId { get; set; }
        public int PantallaId { get; set; }
        public DateTime Fechacrea { get; set; }

        // Relaciones
        public Pantallas Pantalla { get; set; } = null!;
        public Roles Rol { get; set; } = null!;
    }
}