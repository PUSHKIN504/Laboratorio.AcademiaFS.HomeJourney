using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities
{
    public class Usuarios
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Passwordhash { get; set; } = null!;
        public int ColaboradorId { get; set; }
        public bool Esadmin { get; set; }
        public bool Activo { get; set; }

        // Relaciones
        public Colaboradores Colaborador { get; set; } = null!;
    }
}