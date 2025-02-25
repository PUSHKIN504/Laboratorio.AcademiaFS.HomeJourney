using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Viajes
    {
        public int ViajeId { get; set; }
        public int SucursalId { get; set; }
        public int TransportistaId { get; set; }
        public int EstadoId { get; set; }
        public TimeSpan Viajehora { get; set; }  
        public DateTime Viajefecha { get; set; }  
        public decimal Totalkilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public int? MonedaId { get; set; }

        // Relaciones
        public Sucursales Sucursal { get; set; } = null!;
        public Transportistas Transportista { get; set; } = null!;
        public Estados Estado { get; set; } = null!;
        public Monedas? Moneda { get; set; }
        public ICollection<Solicitudesviajes> Solicitudesviajes { get; set; } = new List<Solicitudesviajes>();
        public ICollection<Valoracionesviajes> Valoracionesviajes { get; set; } = new List<Valoracionesviajes>();
        public ICollection<Viajesdetalles> Viajesdetalles { get; set; } = new List<Viajesdetalles>();
    }

    public class ViajesCreateDto
    {
        public int SucursalId { get; set; }
        public int TransportistaId { get; set; }
        public int EstadoId { get; set; }
        public TimeSpan Viajehora { get; set; }
        public DateTime Viajefecha { get; set; }
        public decimal Totalkilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public int Usuariocrea { get; set; }
        public int? MonedaId { get; set; }

        public List<ViajesdetallesCreateDto> Detalles { get; set; } = new List<ViajesdetallesCreateDto>();
    }

    public class ViajesdetallesCreateDto
    {
        public int ColaboradorId { get; set; }
        public decimal Distanciakilometros { get; set; }
        public decimal Totalpagar { get; set; }
        public int ColaboradorsucursalId { get; set; }
        public int Usuariocrea { get; set; }
        public int? MonedaId { get; set; }
    }



}