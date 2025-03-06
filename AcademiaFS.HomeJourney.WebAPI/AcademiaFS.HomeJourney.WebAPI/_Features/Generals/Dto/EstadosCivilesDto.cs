using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    [ExcludeFromCodeCoverage]
    public class EstadoCivilDto
    {
        public int EstadocivilId { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
