using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Sucursales
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
        public int? JefeId { get; set; }

        // Relaciones
        public virtual Colaboradores Jefe { get; set; }
        public ICollection<Colaboradoressucursales> Colaboradoressucursales { get; set; } = new List<Colaboradoressucursales>();
        public ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();
    }
}