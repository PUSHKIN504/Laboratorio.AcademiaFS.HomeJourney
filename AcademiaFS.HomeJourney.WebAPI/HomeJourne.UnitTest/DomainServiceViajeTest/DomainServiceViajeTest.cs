using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using AcademiaFS.HomeJourney.WebAPI._Common;

namespace HomeJourne.UnitTest.DomainServiceViajeTest
{
    public class DomainServiceViajeTests
    {
        private HomeJourneyContext CreateContextWithData(
            List<Colaboradores> colaboradores = null,
            List<Sucursales> sucursales = null,
            List<Colaboradoressucursales> asignaciones = null)
        {
            var options = new DbContextOptionsBuilder<HomeJourneyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new HomeJourneyContext(options);

            if (colaboradores != null)
                context.Colaboradores.AddRange(colaboradores);
            if (sucursales != null)
                context.Sucursales.AddRange(sucursales);
            if (asignaciones != null)
                context.Colaboradoressucursales.AddRange(asignaciones);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ValidateAndSetDistanceAsync_ValidDistance_SetsDistance()
        {
            var colaborador = new Colaboradores
            {
                ColaboradorId = 1,
                Activo = true,
                Latitud = 10.0M,
                Longitud = 20.0M,
                Direccion = "Calle Test 123"
            };

            var sucursal = new Sucursales
            {
                SucursalId = 1,
                Activo = true,
                Latitud = 10.0M,
                Longitud = 20.0M,
                Direccion = "Sucursal Test",
                Nombre = "Sucursal de Prueba"
            };

            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };

            var context = CreateContextWithData(
                colaboradores: new List<Colaboradores> { colaborador },
                sucursales: new List<Sucursales> { sucursal }
            );

            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            double expectedDistance = 30.0;
            var distanceMatrix = new double[2, 2];
            distanceMatrix[0, 1] = expectedDistance;

            googleMapsServiceMock
                .Setup(s => s.GetDistanceMatrixAsync(It.IsAny<List<ViajesdetallesCreateClusteredDto>>()))
                .ReturnsAsync(distanceMatrix);

            var service = new DomainServiceViaje(context, googleMapsServiceMock.Object);

            // Act
            await service.ValidateAndSetDistanceAsync(entity);

            // Assert
            Assert.Equal((decimal)expectedDistance, entity.Distanciakilometro);
        }



        [Fact]
        public async Task ValidateAndSetDistanceAsync_MissingColaborador_ThrowsException()
        {
            // Arrange
            var sucursal = new Sucursales
            {
                SucursalId = 1,
                Activo = true,
                Latitud = 15.0M,
                Longitud = 25.0M,
                Direccion = "Sucursal Test",  
                Nombre = "Sucursal de Prueba" 
            };

            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };

            var context = CreateContextWithData(
                sucursales: new List<Sucursales> { sucursal }
            );

            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var service = new DomainServiceViaje(context, googleMapsServiceMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.ValidateAndSetDistanceAsync(entity));
            Assert.Contains("El colaborador con ID 1 no existe o no está activo", ex.Message);
        }


        [Fact]
        public async Task ValidateAndSetDistanceAsync_MissingSucursal_ThrowsException()
        {
            // Arrange
            var colaborador = new Colaboradores
            {
                ColaboradorId = 1,
                Activo = true,
                Latitud = 10.0M,
                Longitud = 20.0M,
                Direccion = "Calle Test 123" 
            };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };

            var context = CreateContextWithData(
                colaboradores: new List<Colaboradores> { colaborador }
            );
            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var service = new DomainServiceViaje(context, googleMapsServiceMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.ValidateAndSetDistanceAsync(entity));
            Assert.Contains("La sucursal con ID 1 no existe o no está activa", ex.Message);
        }


        [Fact]
        public async Task ValidateAndSetDistanceAsync_ExistingAssignment_ThrowsException()
        {
            // Arrange
            var colaborador = new Colaboradores
            {
                ColaboradorId = 1,
                Activo = true,
                Latitud = 10.0M,
                Longitud = 20.0M,
                Direccion = "Calle Test 123" 
            };

            var sucursal = new Sucursales
            {
                SucursalId = 1,
                Activo = true,
                Latitud = 15.0M,
                Longitud = 25.0M,
                Direccion = "Sucursal Test",
                Nombre = "Sucursal de Prueba"
            };

            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };

            var existingAssignment = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1, Activo = true };

            var context = CreateContextWithData(
                colaboradores: new List<Colaboradores> { colaborador },
                sucursales: new List<Sucursales> { sucursal },
                asignaciones: new List<Colaboradoressucursales> { existingAssignment }
            );

            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var service = new DomainServiceViaje(context, googleMapsServiceMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.ValidateAndSetDistanceAsync(entity));
            Assert.Contains("ya está asignado a la sucursal", ex.Message);
        }

        [Fact]
        public async Task ValidateAndSetDistanceAsync_ZeroOrNegativeDistance_ThrowsException()
        {
            var colaborador = new Colaboradores
            {
                ColaboradorId = 1,
                Activo = true,
                Latitud = 10.0M,
                Longitud = 20.0M,
                Direccion = "Calle Test 123"
            };
            var sucursal = new Sucursales
            {
                SucursalId = 1,
                Activo = true,
                Latitud = 10.0M,
                Longitud = 20.0M,
                Direccion = "Sucursal Test",
                Nombre = "Sucursal de Prueba"
            };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };

            var context = CreateContextWithData(
                colaboradores: new List<Colaboradores> { colaborador },
                sucursales: new List<Sucursales> { sucursal }
            );

            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var distanceMatrix = new double[2, 2];
            distanceMatrix[0, 1] = 0.0; 

            googleMapsServiceMock
                .Setup(s => s.GetDistanceMatrixAsync(It.IsAny<List<ViajesdetallesCreateClusteredDto>>()))
                .ReturnsAsync(distanceMatrix);

            var service = new DomainServiceViaje(context, googleMapsServiceMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.ValidateAndSetDistanceAsync(entity));
            Assert.Contains("La distancia calculada no puede ser cero o negativa", ex.Message);
        }

        [Fact]
        public async Task ValidateAndSetDistanceAsync_DistanceExceedsLimit_ThrowsException()
        {
            // Arrange
            var colaborador = new Colaboradores
            {
                ColaboradorId = 1,
                Activo = true,
                Latitud = 10.0M,
                Longitud = 20.0M,
                Direccion = "Calle Colaborador 123"
            };
            var sucursal = new Sucursales
            {
                SucursalId = 1,
                Activo = true,
                Latitud = 15.0M,
                Longitud = 25.0M,
                Direccion = "Sucursal Test",  
                Nombre = "Sucursal de Prueba" 
            };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };

            var context = CreateContextWithData(
                colaboradores: new List<Colaboradores> { colaborador },
                sucursales: new List<Sucursales> { sucursal }
            );

            var googleMapsServiceMock = new Mock<IGoogleMapsService>();

            double distance = 60.0;
            var distanceMatrix = new double[2, 2];
            distanceMatrix[0, 1] = distance;

            googleMapsServiceMock
                .Setup(s => s.GetDistanceMatrixAsync(It.IsAny<List<ViajesdetallesCreateClusteredDto>>()))
                .ReturnsAsync(distanceMatrix);

            var service = new DomainServiceViaje(context, googleMapsServiceMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.ValidateAndSetDistanceAsync(entity));
            Assert.Contains($"La distancia calculada ({distance} km) excede el límite de 50 km", ex.Message);
        }


    }
}
