using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;

namespace HomeJourne.UnitTest.DomainServiceViajeTest
{
    public class DomainServiceViajeTests
    {

        [Fact]
        public void ValidateAndSetDistance_ValidDistance_SetsDistance()
        {
            // Arrange
            DomainServiceViaje CreateService() => new DomainServiceViaje();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = true };
            var sucursal = new Sucursales { SucursalId = 1, Activo = true };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            decimal validDistance = 30M;
            var domainService = CreateService();

            // Act
            domainService.ValidateAndSetDistance(entity, colaborador, sucursal, validDistance);

            // Assert
            entity.Distanciakilometro.Should().Be(validDistance);
        }

        [Fact]
        public void ValidateAndSetDistance_InactiveColaborador_ThrowsException()
        {
            // Arrange
            DomainServiceViaje CreateService() => new DomainServiceViaje();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = false };
            var sucursal = new Sucursales { SucursalId = 1, Activo = true };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            decimal validDistance = 30M;
            var domainService = CreateService();

            // Act
            Action act = () => domainService.ValidateAndSetDistance(entity, colaborador, sucursal, validDistance);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"El colaborador con ID {colaborador.ColaboradorId} no está activo*");
        }

        [Fact]
        public void ValidateAndSetDistance_InactiveSucursal_ThrowsException()
        {
            // Arrange
            DomainServiceViaje CreateService() => new DomainServiceViaje();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = true };
            var sucursal = new Sucursales { SucursalId = 1, Activo = false };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            decimal validDistance = 30M;
            var domainService = CreateService();

            // Act
            Action act = () => domainService.ValidateAndSetDistance(entity, colaborador, sucursal, validDistance);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"La sucursal con ID {sucursal.SucursalId} no está activa*");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ValidateAndSetDistance_ZeroOrNegativeDistance_ThrowsException(decimal invalidDistance)
        {
            // Arrange
            DomainServiceViaje CreateService() => new DomainServiceViaje();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = true };
            var sucursal = new Sucursales { SucursalId = 1, Activo = true };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            var domainService = CreateService();

            // Act
            Action act = () => domainService.ValidateAndSetDistance(entity, colaborador, sucursal, invalidDistance);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*La distancia calculada no puede ser cero o negativa*");
        }

        [Fact]
        public void ValidateAndSetDistance_DistanceExceedsLimit_ThrowsException()
        {
            // Arrange
            DomainServiceViaje CreateService() => new DomainServiceViaje();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = true };
            var sucursal = new Sucursales { SucursalId = 1, Activo = true };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            decimal excessiveDistance = 60M;
            var domainService = CreateService();

            // Act
            Action act = () => domainService.ValidateAndSetDistance(entity, colaborador, sucursal, excessiveDistance);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"La distancia calculada ({excessiveDistance} km) excede el límite de 50 km*");
        }

        private DomainServiceViaje CreateService() => new DomainServiceViaje();

        #region ValidateAndSetDistance

        [Fact]
        public void ValidateAndSetDistance_WithValidInputs_SetsDistance()
        {
            // Arrange
            var service = CreateService();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = true };
            var sucursal = new Sucursales { SucursalId = 1, Activo = true };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            decimal validDistance = 30M;

            // Act
            Action act = () => service.ValidateAndSetDistance(entity, colaborador, sucursal, validDistance);

            // Assert
            act.Should().NotThrow();
            entity.Distanciakilometro.Should().Be(validDistance);
        }

        [Fact]
        public void ValidateAndSetDistance_WithInactiveColaborador_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = false };
            var sucursal = new Sucursales { SucursalId = 1, Activo = true };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            decimal distance = 30M;

            // Act
            Action act = () => service.ValidateAndSetDistance(entity, colaborador, sucursal, distance);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage($"El colaborador con ID {colaborador.ColaboradorId} no está activo.*");
        }

        [Fact]
        public void ValidateAndSetDistance_WithInactiveSucursal_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = true };
            var sucursal = new Sucursales { SucursalId = 1, Activo = false };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            decimal distance = 30M;

            // Act
            Action act = () => service.ValidateAndSetDistance(entity, colaborador, sucursal, distance);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage($"La sucursal con ID {sucursal.SucursalId} no está activa.*");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ValidateAndSetDistance_WithZeroOrNegativeDistance_ThrowsException(decimal invalidDistance)
        {
            // Arrange
            var service = CreateService();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = true };
            var sucursal = new Sucursales { SucursalId = 1, Activo = true };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };

            // Act
            Action act = () => service.ValidateAndSetDistance(entity, colaborador, sucursal, invalidDistance);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*La distancia calculada no puede ser cero o negativa*");
        }

        [Fact]
        public void ValidateAndSetDistance_WithDistanceExceedingLimit_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var colaborador = new Colaboradores { ColaboradorId = 1, Activo = true };
            var sucursal = new Sucursales { SucursalId = 1, Activo = true };
            var entity = new Colaboradoressucursales { ColaboradorId = 1, SucursalId = 1 };
            decimal excessiveDistance = 60M;

            // Act
            Action act = () => service.ValidateAndSetDistance(entity, colaborador, sucursal, excessiveDistance);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage($"La distancia calculada ({excessiveDistance} km) excede el límite de 50 km*");
        }

        #endregion

        #region ValidateFechaNoEnPasado

        [Fact]
        public void ValidateFechaNoEnPasado_WithFutureOrCurrentDate_DoesNotThrow()
        {
            // Arrange
            var service = CreateService();
            var futureDate = DateTime.Now.AddDays(1);
            var currentDate = DateTime.Now;

            // Act
            Action actFuture = () => service.ValidateFechaNoEnPasado(futureDate, "fecha");
            Action actCurrent = () => service.ValidateFechaNoEnPasado(currentDate, "fecha");

            // Assert
            actFuture.Should().NotThrow();
            actCurrent.Should().NotThrow();
        }

        [Fact]
        public void ValidateFechaNoEnPasado_WithPastDate_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var pastDate = DateTime.Now.AddDays(-1);

            // Act
            Action act = () => service.ValidateFechaNoEnPasado(pastDate, "fecha del viaje");

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("La fecha del viaje no puede estar en el pasado.*");
        }

        #endregion

        #region ValidateCoordenadas

        [Fact]
        public void ValidateCoordenadas_WithValidCoordinates_DoesNotThrow()
        {
            // Arrange
            var service = CreateService();
            decimal lat = 45m, lon = 90m;
            string nombreEntidad = "Sucursal";

            // Act
            Action act = () => service.ValidateCoordenadas(lat, lon, nombreEntidad);

            // Assert
            act.Should().NotThrow();
        }


        [Theory]
        [InlineData(-100, 50)]
        [InlineData(100, 50)]
        public void ValidateCoordenadas_WithInvalidLatitud_ThrowsException(decimal lat, decimal lon)
        {
            // Arrange
            var service = CreateService();
            string nombreEntidad = "Sucursal";

            // Act
            Action act = () => service.ValidateCoordenadas(lat, lon, nombreEntidad);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage($"La latitud de {nombreEntidad} debe estar entre -90 y 90*");
        }

        [Theory]
        [InlineData(50, -200)]
        [InlineData(50, 200)]
        public void ValidateCoordenadas_WithInvalidLongitud_ThrowsException(decimal lat, decimal lon)
        {
            // Arrange
            var service = CreateService();
            string nombreEntidad = "Sucursal";

            // Act
            Action act = () => service.ValidateCoordenadas(lat, lon, nombreEntidad);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage($"La longitud de {nombreEntidad} debe estar entre -180 y 180*");
        }

        #endregion

        #region ValidateDistanceThreshold

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void ValidateDistanceThreshold_WithValidValue_DoesNotThrow(decimal threshold)
        {
            // Arrange
            var service = CreateService();

            // Act
            Action act = () => service.ValidateDistanceThreshold(threshold);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void ValidateDistanceThreshold_WithNegativeValue_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            decimal threshold = -5m;

            // Act
            Action act = () => service.ValidateDistanceThreshold(threshold);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("El umbral de distancia debe ser un valor positivo o cero*");
        }

        #endregion

        #region ValidateTripInputs

        [Fact]
        public void ValidateTripInputs_WithValidInputs_DoesNotThrow()
        {
            // Arrange
            var service = CreateService();
            var tripDto = new ViajesCreateClusteredDto
            {
                SucursalId = 1,
                TransportistaIds = new List<int> { 1, 2 },
                Viajefecha = DateTime.Now.AddDays(1),
                Usuariocrea = 1
            };
            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto>
                {
                    new ViajesdetallesCreateClusteredDto { ColaboradorId = 10 }
                }
            };

            // Act
            Action act = () => service.ValidateTripInputs(tripDto, clusteredEmployees);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void ValidateTripInputs_WithNullTripDto_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            ViajesCreateClusteredDto tripDto = null;
            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto> { new ViajesdetallesCreateClusteredDto { ColaboradorId = 10 } }
            };

            // Act
            Action act = () => service.ValidateTripInputs(tripDto, clusteredEmployees);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Datos del viaje inválidos o incompletos*");
        }


        [Fact]
        public void ValidateTripInputs_WithInvalidSucursalId_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var tripDto = new ViajesCreateClusteredDto
            {
                SucursalId = 0, 
                TransportistaIds = new List<int> { 1 },
                Viajefecha = DateTime.Now.AddDays(1),
                Usuariocrea = 1
            };
            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto> { new ViajesdetallesCreateClusteredDto { ColaboradorId = 10 } }
            };

            // Act
            Action act = () => service.ValidateTripInputs(tripDto, clusteredEmployees);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Datos del viaje inválidos o incompletos*");
        }

        [Fact]
        public void ValidateTripInputs_WithEmptyTransportistaIds_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var tripDto = new ViajesCreateClusteredDto
            {
                SucursalId = 1,
                TransportistaIds = new List<int>(), 
                Viajefecha = DateTime.Now.AddDays(1),
                Usuariocrea = 1
            };
            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto> { new ViajesdetallesCreateClusteredDto { ColaboradorId = 10 } }
            };

            // Act
            Action act = () => service.ValidateTripInputs(tripDto, clusteredEmployees);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Datos del viaje inválidos o incompletos*");
        }

        [Fact]
        public void ValidateTripInputs_WithEmptyClusteredEmployees_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var tripDto = new ViajesCreateClusteredDto
            {
                SucursalId = 1,
                TransportistaIds = new List<int> { 1 },
                Viajefecha = DateTime.Now.AddDays(1),
                Usuariocrea = 1
            };
            List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees = null;

            // Act
            Action act = () => service.ValidateTripInputs(tripDto, clusteredEmployees);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Los empleados clusterizados no pueden estar vacíos*");
        }

        [Fact]
        public void ValidateTripInputs_WithPastViajefecha_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var tripDto = new ViajesCreateClusteredDto
            {
                SucursalId = 1,
                TransportistaIds = new List<int> { 1 },
                Viajefecha = DateTime.Now.AddDays(-1), 
                Usuariocrea = 1
            };
            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto> { new ViajesdetallesCreateClusteredDto { ColaboradorId = 10 } }
            };

            // Act
            Action act = () => service.ValidateTripInputs(tripDto, clusteredEmployees);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("La fecha del viaje no puede estar en el pasado*");
        }

        [Fact]
        public void ValidateTripInputs_WithDuplicateTransportistaIds_ThrowsException()
        {
            // Arrange
            var service = CreateService();
            var tripDto = new ViajesCreateClusteredDto
            {
                SucursalId = 1,
                TransportistaIds = new List<int> { 1, 1, 2 }, 
                Viajefecha = DateTime.Now.AddDays(1),
                Usuariocrea = 1
            };
            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto> { new ViajesdetallesCreateClusteredDto { ColaboradorId = 10 } }
            };

            // Act
            Action act = () => service.ValidateTripInputs(tripDto, clusteredEmployees);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("La lista de transportistas contiene elementos duplicados*");
        }

        #endregion
    }
}
