using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    [ExcludeFromCodeCoverage]
    public class ViajeDetalleDto
    {
        public int ViajedetalleId { get; set; }
        public int ViajeId { get; set; }
        public int ColaboradorId { get; set; }
        public decimal Distanciakilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public int ColaboradorsucursalId { get; set; }
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public int? MonedaId { get; set; }
    }
}
