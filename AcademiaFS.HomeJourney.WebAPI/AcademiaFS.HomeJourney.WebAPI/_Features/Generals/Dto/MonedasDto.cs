using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    [ExcludeFromCodeCoverage]
    public class MonedaDto
    {
        public int MonedaId { get; set; }
        public string? Nombre { get; set; }
        public string? Simbolo { get; set; }
        public decimal? ValorLempiras { get; set; }
    }
}
