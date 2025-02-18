using AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;
using Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName.Maps._Common;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney
{
    public class HomeJourneyContext : DbContext
    {
        public HomeJourneyContext(DbContextOptions<HomeJourneyContext> options) : base(options)
        {

        }

        //public DbSet<Usuario> Usuarios => Set<Usuario>();

        public  DbSet<Cargos> Cargos { get; set; }

        public  DbSet<Ciudades> Ciudades { get; set; }

        public  DbSet<Colaboradore> Colaboradores { get; set; }

        public  DbSet<Colaboradoressucursale> Colaboradoressucursales { get; set; }

        public  DbSet<Departamento> Departamentos { get; set; }

        public  DbSet<Estado> Estados { get; set; }

        public  DbSet<Estadoscivile> Estadosciviles { get; set; }

        public  DbSet<Moneda> Monedas { get; set; }

        public  DbSet<Paise> Paises { get; set; }

        public  DbSet<Pantalla> Pantallas { get; set; }

        public  DbSet<Pantallasrole> Pantallasroles { get; set; }

        public  DbSet<Persona> Personas { get; set; }

        public  DbSet<Role> Roles { get; set; }

        public  DbSet<Serviciostransporte> Serviciostransportes { get; set; }

        public  DbSet<Solicitudesviaje> Solicitudesviajes { get; set; }

        public  DbSet<Sucursale> Sucursales { get; set; }

        public  DbSet<Transportista> Transportistas { get; set; }

        public  DbSet<Usuario> Usuarios { get; set; }

        public  DbSet<Valoracionesviaje> Valoracionesviajes { get; set; }

        public  DbSet<Viaje> Viajes { get; set; }

        public  DbSet<Viajesdetalle> Viajesdetalles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UsuarioMap());

        }
    }
}
