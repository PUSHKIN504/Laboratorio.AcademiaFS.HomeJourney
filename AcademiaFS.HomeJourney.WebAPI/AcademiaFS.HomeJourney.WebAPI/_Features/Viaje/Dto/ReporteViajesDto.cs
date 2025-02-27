namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    public class ViajesTransportistaReporteDto
    {
        public int TransportistaId { get; set; }
        public string TransportistaNombre { get; set; } // Nombre de la Persona
        public string DNI { get; set; } // DNI de la Persona
        public string Correo { get; set; } // Correo de la Persona
        public List<ViajeDetalleReporteDto> Viajes { get; set; } = new List<ViajeDetalleReporteDto>();
        public decimal TotalPagar { get; set; }
    }

    public class ViajeDetalleReporteDto
    {
        public int ViajeId { get; set; }
        public DateTime Viajefecha { get; set; }
        public TimeSpan Viajehora { get; set; }
        public decimal Totalkilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public List<ViajesdetallesReporteDto> Detalles { get; set; } = new List<ViajesdetallesReporteDto>();
    }

    public class ViajesdetallesReporteDto
    {
        public int ColaboradorId { get; set; }
        public decimal Distanciakilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public int ColaboradorsucursalId { get; set; }
    }
}
