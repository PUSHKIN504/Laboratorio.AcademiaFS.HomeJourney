using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Colaboradoressucursales : IActivableInterface
    {
        public int ColaboradorsucursalId { get; set; }
        public int ColaboradorId { get; set; }
        public int SucursalId { get; set; }
        public decimal Distanciakilometro { get; set; }
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }


        // Relaciones
        public Colaboradores Colaborador { get; set; } = null!;
        public Sucursales Sucursal { get; set; } = null!;
        public ICollection<Viajesdetalles> Viajesdetalles { get; set; } = new List<Viajesdetalles>();
    }
}