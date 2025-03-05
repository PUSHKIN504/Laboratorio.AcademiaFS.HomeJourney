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
            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HomeJourneyContext>();

                db.Database.EnsureDeleted(); 
                db.Database.EnsureCreated(); 

                SeedTestData(db); 
            }
        });
    }

    private void SeedTestData(HomeJourneyContext db)
    {
        var fechaActual = DateTime.UtcNow; 

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

        var colaborador = new Colaboradores
        {
            ColaboradorId = 1,
            Direccion = "Calle Falsa 123",
            CargoId = 3,
            Persona = persona,
            Cargo = new Cargos { CargoId = 1, Nombre = "Cargo de Prueba" },
            Rol = new Roles { RolId = 1, Nombre = "Rol de Prueba" },
            Fechacrea = fechaActual,
            Fechamodifica = null
        };

        var sucursalActiva = new Sucursales
        {
            SucursalId = 1,
            Nombre = "Sucursal de Prueba",
            Direccion = "Avenida Principal #100",
            Activo = true,
            Usuariocrea = 1,
            Fechacrea = fechaActual,
            Fechamodifica = null
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
        db.Colaboradores.Add(colaborador);
        db.Sucursales.Add(sucursalActiva);
        db.Colaboradoressucursales.Add(colaboradorSucursal);
        db.Usuarios.Add(usuario);

        db.SaveChanges();
    }


}
