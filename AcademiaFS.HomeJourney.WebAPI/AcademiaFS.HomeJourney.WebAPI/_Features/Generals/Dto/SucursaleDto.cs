using System.Diagnostics.CodeAnalysis;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    [ExcludeFromCodeCoverage]
    public class SucursalesDto
    {
        public int SucursalId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }
}
