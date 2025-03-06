using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Security.Cryptography;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<HomeJourneyContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<HomeJourneyContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase")
                       .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HomeJourneyContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                if (!db.Personas.Any() && !db.Colaboradores.Any() && !db.Sucursales.Any() && !db.Colaboradoressucursales.Any() && !db.Usuarios.Any() && !db.Transportistas.Any())
                    SeedTestData(db);
            }
        });
    }

    private void SeedTestData(HomeJourneyContext db)
    {
        var fechaActual = DateTime.UtcNow;

        var cargo = new Cargos { CargoId = 3, Nombre = "Cargo de Prueba" };
        var rol = new Roles { RolId = 2, Nombre = "Rol de Prueba" };

        var persona = new Personas
        {
            PersonaId = 1,
            Nombre = "Nombre de Prueba",
            Apelllido = "Apellido de Prueba",
            Documentonacionalidentificacion = "12345678",
            Email = "test@example.com",
            Sexo = "M",
            Fechacrea = fechaActual,
            Fechamodifica = null
        };

        var persona2 = new Personas
        {
            PersonaId = 2,
            Nombre = "Nombre de Prueba 2",
            Apelllido = "Apellido de Prueba 2",
            Documentonacionalidentificacion = "87654321",
            Email = "test2@example.com",
            Sexo = "F",
            Fechacrea = fechaActual,
            Fechamodifica = null
        };

        var transportista = new Transportistas
        {
            TransportistaId = 10,
            Activo = true,
            Tarifaporkilometro = 5.0m,
        };

        var colaborador = new Colaboradores
        {
            ColaboradorId = 1,
            Direccion = "Calle Falsa 123",
            CargoId = 3,
            Persona = persona,
            Cargo = cargo,  
            Rol = rol,      
            Fechacrea = fechaActual,
            Fechamodifica = null,
            Latitud = 15.557985970431286M,
            Longitud = -87.993208536194170M,
            Activo = true
        };

        var colaborador2 = new Colaboradores
        {
            ColaboradorId = 2,
            Direccion = "Otra Calle 456",
            CargoId = 3,
            Persona = persona2,
            Cargo = cargo,  
            Rol = rol,      
            Fechacrea = fechaActual,
            Fechamodifica = null,
            Latitud= 15.475165044215684M,
            Longitud = -87.980962306614130M,
            Activo = true
        };

        var sucursalActiva = new Sucursales
        {
            SucursalId = 1,
            Nombre = "Sucursal de Prueba",
            Direccion = "Avenida Principal #100",
            Activo = true,
            Usuariocrea = 1,
            Fechacrea = fechaActual,
            Fechamodifica = null,
            Longitud = -88.026969291986460M,
            Latitud = 15.502893036915468M,
        };

        var colaboradorSucursal = new Colaboradoressucursales
        {
            ColaboradorId = colaborador.ColaboradorId,
            Colaborador = colaborador,
            SucursalId = sucursalActiva.SucursalId,
            Sucursal = sucursalActiva,
            Activo = true,
            Usuariocrea = 1,
            Fechacrea = fechaActual
        };
        colaborador.Colaboradoressucursales.Add(colaboradorSucursal);


        byte[] passwordHash;
        using (var sha256 = SHA256.Create())
        {
            passwordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes("testpassword"));
        }

        var usuario = new Usuarios
        {
            UsuarioId = 1,
            Username = "testuser",
            Passwordhash = passwordHash,
            Activo = true,
            Colaborador = colaborador,
        };

        db.Personas.Add(persona);
        db.Personas.Add(persona2);
        db.Colaboradores.Add(colaborador);
        db.Colaboradores.Add(colaborador2);
        db.Sucursales.Add(sucursalActiva);
        db.Colaboradoressucursales.Add(colaboradorSucursal);
        db.Usuarios.Add(usuario);
        db.Transportistas.Add(transportista);

        db.SaveChanges();
    }

}
