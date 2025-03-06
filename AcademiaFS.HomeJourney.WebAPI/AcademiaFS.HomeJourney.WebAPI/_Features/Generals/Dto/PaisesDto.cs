
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    [ExcludeFromCodeCoverage]
    public class PaisesDto
    {
            public int PaisId { get; set; }
            [Required]
            public string? Nombre { get; set; }
            [Required]

            public bool? Activo { get; set; }

    }
}
