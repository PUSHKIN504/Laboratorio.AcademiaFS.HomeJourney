using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    [ExcludeFromCodeCoverage]
    public class ViajesdetallesCreateClusteredDto
    {
        public int ColaboradorId { get; set; }
        public decimal Distanciakilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public int ColaboradorsucursalId { get; set; }
        public int? MonedaId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class ViajesCreateClusteredDto
    {
        public int SucursalId { get; set; }
        public List<int> TransportistaIds { get; set; } = new List<int>();
        public int EstadoId { get; set; }
        public TimeSpan Viajehora { get; set; }
        public DateTime Viajefecha { get; set; }
        public int Usuariocrea { get; set; }
        public int? MonedaId { get; set; }
    }
}
