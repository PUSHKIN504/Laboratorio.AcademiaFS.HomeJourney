using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    public class SolicitudViajeDto
    {
        public int SolicitudviajeId { get; set; }
        public int ColaboradorId { get; set; }
        public DateOnly Fechasolicitud { get; set; }
        public int ViajeId { get; set; }
        public int EstadoId { get; set; }
        public string? Comentarios { get; set; }
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public int? SupervisorId { get; set; }
        public DateTime? FechaAprobacion { get; set; }

        public Colaboradores? Supervisor { get; set; }
    }

    public class SolicitudViajeAprobacionDto
    {
        public int SolicitudviajeId { get; set; }
        public int SupervisorId { get; set; } 
        public bool Aprobar { get; set; } 
        public string? Comentarios { get; set; }
    }
    public class SolicitudViajeCreateDto
    {
        public int ColaboradorId { get; set; }
        public int ViajeId { get; set; }
        public string? Comentarios { get; set; }
    }
}
