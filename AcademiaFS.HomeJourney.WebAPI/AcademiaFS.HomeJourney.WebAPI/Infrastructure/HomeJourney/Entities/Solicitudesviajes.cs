using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Solicitudesviajes
    {
        public int SolicitudviajeId { get; set; }
        public int ColaboradorId { get; set; }
        public DateTime Fechasolicitud { get; set; } // Mapeada a date
        public int ViajeId { get; set; }
        public int EstadoId { get; set; }
        public string? Comentarios { get; set; }
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }

        // Relaciones
        public Colaboradores Colaborador { get; set; } = null!;
        public Viajes Viaje { get; set; } = null!;
        public Estados Estado { get; set; } = null!;
    }
}