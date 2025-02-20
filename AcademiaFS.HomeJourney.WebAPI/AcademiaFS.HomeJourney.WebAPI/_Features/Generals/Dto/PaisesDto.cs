
using System.ComponentModel.DataAnnotations;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    public class PaisesDto
    {
            public int PaisId { get; set; }
            [Required]
            public string? Nombre { get; set; }
            [Required]

            public bool? Activo { get; set; }

    }
}
