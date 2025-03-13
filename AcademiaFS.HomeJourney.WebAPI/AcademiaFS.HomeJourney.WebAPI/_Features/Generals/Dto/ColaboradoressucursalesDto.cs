using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    [ExcludeFromCodeCoverage]
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
        [BindNever]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string NombreColaborador { get; set; } = string.Empty;

        [BindNever]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string NombreSucursal { get; set; } = string.Empty;
    }
    [ExcludeFromCodeCoverage]
    public class ColaboradorSucursalRequestDto 
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
