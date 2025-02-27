using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Auth;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Viaje;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney
{
    public class HomeJourneyContext : DbContext
    {
        public HomeJourneyContext(DbContextOptions<HomeJourneyContext> options) : base(options)
        {

        }


        public DbSet<Cargos> Cargos { get; set; }

        public DbSet<Ciudades> Ciudades { get; set; }

        public DbSet<Colaboradores> Colaboradores { get; set; }

        public DbSet<Colaboradoressucursales> Colaboradoressucursales { get; set; }

        public DbSet<Departamentos> Departamentos { get; set; }

        public DbSet<Estados> Estados { get; set; }

        public DbSet<Estadosciviles> Estadosciviles { get; set; }

        public DbSet<Monedas> Monedas { get; set; }

        public DbSet<Pantallas> Pantallas { get; set; }

        public DbSet<Pantallasroles> Pantallasroles { get; set; }

        public DbSet<Personas> Personas { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Serviciostransportes> Serviciostransportes { get; set; }

        public DbSet<Solicitudesviajes> Solicitudesviajes { get; set; }

        public DbSet<Sucursales> Sucursales { get; set; }

        public DbSet<Transportistas> Transportistas { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Valoracionesviajes> Valoracionesviajes { get; set; }

        public DbSet<Viajes> Viajes { get; set; }

        public DbSet<Viajesdetalles> Viajesdetalles { get; set; }
        public DbSet<Paises> Paises { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaisesMap());
            modelBuilder.ApplyConfiguration(new CargosMap());
            modelBuilder.ApplyConfiguration(new CiudadesMap());
            modelBuilder.ApplyConfiguration(new ColaboradoresMap());
            modelBuilder.ApplyConfiguration(new ColaboradoressucursalesMap());
            modelBuilder.ApplyConfiguration(new DepartamentosMap());
            modelBuilder.ApplyConfiguration(new EstadosMap());
            modelBuilder.ApplyConfiguration(new EstadoscivilesMap());
            modelBuilder.ApplyConfiguration(new MonedasMap());
            modelBuilder.ApplyConfiguration(new PantallasMap());
            modelBuilder.ApplyConfiguration(new PantallasrolesMap());
            modelBuilder.ApplyConfiguration(new PersonasMap());
            modelBuilder.ApplyConfiguration(new RolesMap());
            modelBuilder.ApplyConfiguration(new ServiciostransportesMap());
            modelBuilder.ApplyConfiguration(new SolicitudesviajesMap());
            modelBuilder.ApplyConfiguration(new SucursalesMap());
            modelBuilder.ApplyConfiguration(new TransportistasMap());
            modelBuilder.ApplyConfiguration(new UsuariosMap());
            modelBuilder.ApplyConfiguration(new ValoracionesviajesMap());
            modelBuilder.ApplyConfiguration(new ViajesMap());
            modelBuilder.ApplyConfiguration(new ViajesdetallesMap());

        }
    }
}
