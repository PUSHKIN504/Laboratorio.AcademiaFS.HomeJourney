using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Personas
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apelllido { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Documentonacionalidentificacion { get; set; } = null!;
        public bool Activo { get; set; }
        public int? EstadocivilId { get; set; }
        public int CiudadId { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }

        // Relaciones existentes
        public Ciudades Ciudad { get; set; } = null!;
        public Estadosciviles? Estadocivil { get; set; }
        //public Transportistas? Transportista { get; set; }

        // Agrega la propiedad de navegación para la relación uno a uno con Colaboradores
        //public Colaboradores? Colaborador { get; set; }
        public ICollection<Colaboradores> Colaborador { get; set; } = new List<Colaboradores>();
        public ICollection<Transportistas> Transportista { get; set; } = new List<Transportistas>();

    }
}