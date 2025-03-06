using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    [ExcludeFromCodeCoverage]
    public class DepartamentoDto
    {
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Activo { get; set; }
        public int? PaisId { get; set; }
    }
}
