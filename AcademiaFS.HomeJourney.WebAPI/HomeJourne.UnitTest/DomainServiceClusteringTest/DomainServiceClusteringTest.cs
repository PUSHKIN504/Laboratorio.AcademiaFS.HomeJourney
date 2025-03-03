using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using AcademiaFS.HomeJourney.WebAPI._Common;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;

namespace AcademiaFS.HomeJourney.Tests
{
    public class DomainServiceClusteringTests
    {
        private HomeJourneyContext CreateContextWithData(
            List<Transportistas> transportistas = null)
        {
            var options = new DbContextOptionsBuilder<HomeJourneyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new HomeJourneyContext(options);

            if (transportistas != null)
                context.Transportistas.AddRange(transportistas);

            context.SaveChanges();
            return context;
        }

        public class TestableDomainServiceClustering : DomainServiceClustering
        {
            public TestableDomainServiceClustering(IGoogleMapsService googleMapsService, HomeJourneyContext context)
                : base(googleMapsService, context)
            { }

            public new List<List<int>> AdjustClustersForDistanceLimit(
                List<List<int>> clusters,
                List<ViajesdetallesCreateClusteredDto> employees,
                decimal maxDistance)
            {
                return base.AdjustClustersForDistanceLimit(clusters, employees, maxDistance);
            }

            public new List<List<int>> PerformHierarchicalClustering(double[,] distances, double maxDistance)
            {
                return base.PerformHierarchicalClustering(distances, maxDistance);
            }

            public new List<List<ViajesdetallesCreateClusteredDto>> MapClustersToEmployees(
                List<ViajesdetallesCreateClusteredDto> employees, List<List<int>> clusters)
            {
                return base.MapClustersToEmployees(employees, clusters);
            }

            public new int AssignTransportista(List<int> availableTransportistas)
            {
                return base.AssignTransportista(availableTransportistas);
            }
        }

        #region Pruebas para ClusterEmployeesAsync

        [Fact]
        public async Task ClusterEmployeesAsync_InvalidDistanceThreshold_ThrowsArgumentException()
        {
            // Arrange
            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var context = CreateContextWithData();
            var service = new DomainServiceClustering(googleMapsServiceMock.Object, context);
            var employees = new List<ViajesdetallesCreateClusteredDto>
            {
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 1, Distanciakilometros = 10m },
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.ClusterEmployeesAsync(employees, 0));
        }

        [Fact]
        public async Task ClusterEmployeesAsync_EmptyEmployeeList_ThrowsArgumentException()
        {
            // Arrange
            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var context = CreateContextWithData();
            var service = new DomainServiceClustering(googleMapsServiceMock.Object, context);
            var employees = new List<ViajesdetallesCreateClusteredDto>();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.ClusterEmployeesAsync(employees, 50m));
        }

        [Fact]
        public async Task ClusterEmployeesAsync_ValidInput_ReturnsMappedClusters()
        {
            // Arrange
            var employees = new List<ViajesdetallesCreateClusteredDto>
            {
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 1, Distanciakilometros = 20m },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 2, Distanciakilometros = 30m },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 3, Distanciakilometros = 10m },
            };

            double[,] distanceMatrix = new double[3, 3]
            {
                { 0, 30, 80 },
                { 30, 0, 90 },
                { 80, 90, 0 }
            };

            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            googleMapsServiceMock.Setup(s => s.GetDistanceMatrixAsync(employees))
                .ReturnsAsync(distanceMatrix);

            var context = CreateContextWithData();
            var service = new DomainServiceClustering(googleMapsServiceMock.Object, context);

            decimal distanceThreshold = 50m; 
            // Act
            var result = await service.ClusterEmployeesAsync(employees, distanceThreshold);

        
            Assert.Equal(2, result.Count);

            var cluster1 = result.FirstOrDefault(c => c.Any(e => e.ColaboradorId == 1));
            var cluster2 = result.FirstOrDefault(c => c.Any(e => e.ColaboradorId == 3));

            Assert.NotNull(cluster1);
            Assert.NotNull(cluster2);

            Assert.Equal(new List<int> { 1, 2 }, cluster1.Select(e => e.ColaboradorId).OrderBy(id => id).ToList());
            Assert.Equal(new List<int> { 3 }, cluster2.Select(e => e.ColaboradorId).ToList());
        }

        #endregion

        #region Pruebas para CreateTripsFromClusters

        [Fact]
        public async Task CreateTripsFromClusters_NullTripDto_ThrowsArgumentNullExceptionAsync()
        {
            // Arrange
            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var context = CreateContextWithData();
            var service = new DomainServiceClustering(googleMapsServiceMock.Object, context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                Task.Run(() => service.CreateTripsFromClusters(null, new List<List<ViajesdetallesCreateClusteredDto>>())));
        }


        [Fact]
        public void CreateTripsFromClusters_InsufficientTransportistas_ThrowsArgumentException()
        {
            // Arrange
            var tripDto = new ViajesCreateClusteredDto
            {
                TransportistaIds = new List<int> { 1 }, 
                SucursalId = 1,
                Viajehora = DateTime.Now.TimeOfDay,
                Viajefecha = DateTime.Today,
                Usuariocrea = 1,
                MonedaId = 1
            };

            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto>
                {
                    new ViajesdetallesCreateClusteredDto { ColaboradorId = 10, Distanciakilometros = 10m, ColaboradorsucursalId = 100, MonedaId = 1 }
                },
                new List<ViajesdetallesCreateClusteredDto>
                {
                    new ViajesdetallesCreateClusteredDto { ColaboradorId = 11, Distanciakilometros = 20m, ColaboradorsucursalId = 101, MonedaId = 1 }
                }
            };

            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var context = CreateContextWithData();
            var service = new DomainServiceClustering(googleMapsServiceMock.Object, context);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => service.CreateTripsFromClusters(tripDto, clusteredEmployees));
            Assert.Contains("No hay suficientes transportistas", ex.Message);
        }

        [Fact]
        public void CreateTripsFromClusters_ValidInput_ReturnsTrips()
        {
            // Arrange
            var transportista = new Transportistas
            {
                TransportistaId = 1,
                Activo = true,
                Tarifaporkilometro = 2.5m
            };
            var context = CreateContextWithData(transportistas: new List<Transportistas> { transportista });
            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var service = new DomainServiceClustering(googleMapsServiceMock.Object, context);

            var tripDto = new ViajesCreateClusteredDto
            {
                SucursalId = 1,
                Viajehora = DateTime.Now.TimeOfDay,
                Viajefecha = DateTime.Today,
                Usuariocrea = 1,
                MonedaId = 1,
                TransportistaIds = new List<int> { 1 }
            };

            var employeeDto = new ViajesdetallesCreateClusteredDto
            {
                ColaboradorId = 10,
                Distanciakilometros = 10m,
                ColaboradorsucursalId = 100,
                MonedaId = 1
            };

            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto> { employeeDto }
            };

            // Act
            var trips = service.CreateTripsFromClusters(tripDto, clusteredEmployees);

            // Assert
            Assert.Single(trips);
            var trip = trips.First();
            Assert.Equal(1, trip.TransportistaId);
            Assert.Equal(10m, trip.Totalkilometros);
            Assert.Equal(25m, trip.Totalpagar); 
            Assert.Single(trip.Viajesdetalles);
        }

        #endregion

        #region Pruebas para métodos expuestos en TestableDomainServiceClustering

        [Fact]
        public void AdjustClustersForDistanceLimit_SplitsCluster_WhenExceedingMaxDistance()
        {
            // Arrange
            var employees = new List<ViajesdetallesCreateClusteredDto>
            {
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 1, Distanciakilometros = 10m },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 2, Distanciakilometros = 30m },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 3, Distanciakilometros = 25m }
            };

            var clusters = new List<List<int>> { new List<int> { 0, 1, 2 } };
            decimal maxDistance = 40m;
            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var context = CreateContextWithData();
            var service = new TestableDomainServiceClustering(googleMapsServiceMock.Object, context);

            // Act
            var adjustedClusters = service.AdjustClustersForDistanceLimit(clusters, employees, maxDistance);

            // Assert
            Assert.Equal(2, adjustedClusters.Count);
            Assert.Equal(new List<int> { 0, 2 }, adjustedClusters[0]);
            Assert.Equal(new List<int> { 1 }, adjustedClusters[1]);
        }

        [Fact]
        public void PerformHierarchicalClustering_MergesClusters_BasedOnDistanceThreshold()
        {
            // Arrange
            double[,] distances = new double[,]
            {
                { 0, 20, 80 },
                { 20, 0, 90 },
                { 80, 90, 0 }
            };
            double maxDistance = 50;
            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var context = CreateContextWithData();
            var service = new TestableDomainServiceClustering(googleMapsServiceMock.Object, context);

            // Act
            var clusters = service.PerformHierarchicalClustering(distances, maxDistance);

            // Assert
            Assert.Equal(2, clusters.Count);
            Assert.Contains(clusters, c => c.Contains(0) && c.Contains(1));
            Assert.Contains(clusters, c => c.Contains(2));
        }

        [Fact]
        public void MapClustersToEmployees_ReturnsCorrectMapping()
        {
            // Arrange
            var employees = new List<ViajesdetallesCreateClusteredDto>
            {
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 1, Distanciakilometros = 10m },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 2, Distanciakilometros = 20m },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 3, Distanciakilometros = 30m }
            };

            var clusters = new List<List<int>>
            {
                new List<int> { 0, 2 },
                new List<int> { 1 }
            };

            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var context = CreateContextWithData();
            var service = new TestableDomainServiceClustering(googleMapsServiceMock.Object, context);

            // Act
            var mappedClusters = service.MapClustersToEmployees(employees, clusters);

            // Assert
            Assert.Equal(2, mappedClusters.Count);
            Assert.Equal(new List<int> { 1, 3 }, mappedClusters[0].Select(e => e.ColaboradorId).OrderBy(id => id).ToList());
            Assert.Single(mappedClusters[1]);
            Assert.Equal(2, mappedClusters[1][0].ColaboradorId);
        }

        [Fact]
        public void AssignTransportista_ReturnsValidTransportistaAndRemovesIt()
        {
            // Arrange
            var availableTransportistas = new List<int> { 1, 2, 3 };
            var googleMapsServiceMock = new Mock<IGoogleMapsService>();
            var context = CreateContextWithData();
            var service = new TestableDomainServiceClustering(googleMapsServiceMock.Object, context);

            // Act
            int assigned = service.AssignTransportista(availableTransportistas);

            // Assert
            Assert.Contains(assigned, new List<int> { 1, 2, 3 });
            Assert.Equal(2, availableTransportistas.Count);
            Assert.DoesNotContain(assigned, availableTransportistas);
        }

        #endregion
    }
}
