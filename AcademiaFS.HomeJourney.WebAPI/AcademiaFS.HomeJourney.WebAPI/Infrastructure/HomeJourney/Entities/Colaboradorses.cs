using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities

{
    public class Colaboradores
    {
        public int ColaboradorId { get; set; }
        public int PersonaId { get; set; }
        public int RolId { get; set; }
        public int CargoId { get; set; }
        public bool Activo { get; set; }
        public string Direccion { get; set; } = null!;
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

        // Relaciones
        public Personas Persona { get; set; } = null!;
        public Roles Rol { get; set; } = null!;
        public Cargos Cargo { get; set; } = null!;
        public ICollection<Colaboradoressucursales> Colaboradoressucursales { get; set; } = new List<Colaboradoressucursales>();
        public ICollection<Solicitudesviajes> Solicitudesviajes { get; set; } = new List<Solicitudesviajes>();
        public ICollection<Valoracionesviajes> Valoracionesviajes { get; set; } = new List<Valoracionesviajes>();

        public ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
    }
}
