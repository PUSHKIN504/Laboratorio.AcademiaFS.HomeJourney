using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    [ExcludeFromCodeCoverage]
    public class ValoracionViajeDto
    {
        public int ValoracionviajeId { get; set; }
        public byte Valoracionnota { get; set; }
        public int ColaboradorId { get; set; }
        public int ViajeId { get; set; }
    }
}
