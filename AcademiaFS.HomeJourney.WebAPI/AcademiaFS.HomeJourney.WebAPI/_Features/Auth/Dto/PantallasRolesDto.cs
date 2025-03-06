using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto
{
    [ExcludeFromCodeCoverage]
    public class PantallasrolesDto
    {
        public int PantallarolId { get; set; }
        public int RolId { get; set; }
        public int PantallaId { get; set; }
        public DateTime Fechacrea { get; set; }
    }
}
