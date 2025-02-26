using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    public class ColaboradorSucursalDto : IActivableInterface
    {
        public int ColaboradorsucursalId { get; set; }
        public int ColaboradorId { get; set; }
        public int SucursalId { get; set; }
        public decimal DistanciaKilometro { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int? UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}
