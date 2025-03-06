using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto
{
    [ExcludeFromCodeCoverage]
    public class RolesDto
    {
        public int RolId { get; set; }
        public string Nombre { get; set; } = null!;
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public bool Activo { get; set; }
    }
}
