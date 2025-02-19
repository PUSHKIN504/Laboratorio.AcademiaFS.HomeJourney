namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    public class ViajeDto
    {
        public int ViajeId { get; set; }
        public int SucursalId { get; set; }
        public int TransportistaId { get; set; }
        public int EstadoId { get; set; }
        public TimeOnly Viajehora { get; set; }
        public DateOnly Viajefecha { get; set; }
        public decimal Totalkilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public int? MonedaId { get; set; }
    }
}
