using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Viajesdetalles
    {
        public int ViajedetalleId { get; set; }
        public int ViajeId { get; set; }
        public int ColaboradorId { get; set; }
        public decimal Distanciakilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public int ColaboradorsucursalId { get; set; }

        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public int? MonedaId { get; set; }

        // Relaciones
        public Viajes Viaje { get; set; } = null!;
        public Colaboradoressucursales Colaboradorsucursal { get; set; } = null!;
        public Monedas? Moneda { get; set; }


        //public ICollection<Colaboradoressucursales> Colaboradoressucursales { get; set; } = new List<Colaboradoressucursales>();

    }

}