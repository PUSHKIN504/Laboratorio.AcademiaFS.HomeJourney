using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;

namespace AcademiaFS.HomeJourney.Tests
{
    public class DomainServiceClusteringTests
    {

        [Fact]
        public void AdjustClustersForDistanceLimit_SplitsCluster_WhenExceedingMaxDistance()
        {
            // Arrange
            DomainServiceClustering CreateService() => new DomainServiceClustering();
            var service = CreateService();
            var employees = new List<ViajesdetallesCreateClusteredDto>
            {
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 1, Distanciakilometros = 10m },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 2, Distanciakilometros = 30m },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 3, Distanciakilometros = 25m }
            };
            var clusters = new List<List<int>> { new List<int> { 0, 1, 2 } };
            decimal maxDistance = 40m;

            // Act
            var adjustedClusters = service.AdjustClustersForDistanceLimit(clusters, employees, maxDistance);

            // Assert
            adjustedClusters.Count.Should().Be(2);
            adjustedClusters[0].Should().Equal(new List<int> { 0, 2 });
            adjustedClusters[1].Should().Equal(new List<int> { 1 });
        }

        [Fact]
        public void PerformHierarchicalClustering_MergesClusters_BasedOnThreshold()
        {
            // Arrange
            DomainServiceClustering CreateService() => new DomainServiceClustering();
            var service = CreateService();
            double[,] distances = new double[,]
            {
                { 0, 10, 100 },
                { 10, 0, 100 },
                { 100, 100, 0 }
            };
            double maxDistance = 50;

            // Act
            var clusters = service.PerformHierarchicalClustering(distances, maxDistance);

            // Assert
            clusters.Count.Should().Be(2);
            clusters.Should().Contain(c => c.Contains(0) && c.Contains(1));
            clusters.Should().Contain(c => c.Contains(2));
        }

        [Fact]
        public void MapClustersToEmployees_ReturnsCorrectMapping()
        {
            // Arrange
            DomainServiceClustering CreateService() => new DomainServiceClustering();
            var service = CreateService();
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

            // Act
            var mappedClusters = service.MapClustersToEmployees(employees, clusters);

            // Assert
            mappedClusters.Count.Should().Be(2);
            mappedClusters[0].Select(e => e.ColaboradorId)
                .OrderBy(id => id)
                .Should().Equal(new List<int> { 1, 3 });
            mappedClusters[1].Should().HaveCount(1);
            mappedClusters[1][0].ColaboradorId.Should().Be(2);
        }

        [Fact]
        public void BuildTripDetails_ComputesTotalPagarCorrectly()
        {
            // Arrange
            DomainServiceClustering CreateService() => new DomainServiceClustering();
            var service = CreateService();
            var tripDto = new ViajesCreateClusteredDto { Usuariocrea = 123 };
            var cluster = new List<ViajesdetallesCreateClusteredDto>
            {
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 1, Distanciakilometros = 10m, ColaboradorsucursalId = 100, MonedaId = 1 },
                new ViajesdetallesCreateClusteredDto { ColaboradorId = 2, Distanciakilometros = 20m, ColaboradorsucursalId = 101, MonedaId = 1 }
            };
            decimal tarifa = 2.5m;

            // Act
            var tripDetails = service.BuildTripDetails(cluster, tripDto, tarifa);

            // Assert
            tripDetails.Should().HaveCount(2);
            tripDetails[0].Totalpagar.Should().Be(25m);
            tripDetails[1].Totalpagar.Should().Be(50m);
            tripDetails.ForEach(d =>
            {
                d.Activo.Should().BeTrue();
                d.Usuariocrea.Should().Be(123);
            });
        }

        [Fact]
        public void AssignTransportista_ReturnsValidIdAndRemovesIt()
        {
            // Arrange
            DomainServiceClustering CreateService() => new DomainServiceClustering();
            var service = CreateService();
            var availableTransportistas = new List<int> { 1, 2, 3 };

            // Act
            int assigned = service.AssignTransportista(availableTransportistas);

            // Assert
            new List<int> { 1, 2, 3 }.Should().Contain(assigned);
            availableTransportistas.Should().HaveCount(2);
            availableTransportistas.Should().NotContain(assigned);
        }

        [Fact]
        public void BuildTrip_CreatesCorrectTrip()
        {
            // Arrange
            DomainServiceClustering CreateService() => new DomainServiceClustering();
            var service = CreateService();
            var tripDto = new ViajesCreateClusteredDto
            {
                SucursalId = 10,
                Viajehora = new TimeSpan(8, 30, 0),
                Viajefecha = new DateTime(2025, 3, 4),
                Usuariocrea = 99,
                MonedaId = 1
            };
            var tripDetails = new List<Viajesdetalles>
            {
                new Viajesdetalles { Distanciakilometros = 10m, Totalpagar = 25m },
                new Viajesdetalles { Distanciakilometros = 20m, Totalpagar = 50m }
            };
            int transportistaId = 5;

            // Act
            var trip = service.BuildTrip(tripDto, tripDetails, transportistaId);

            // Assert
            trip.SucursalId.Should().Be(tripDto.SucursalId);
            trip.TransportistaId.Should().Be(transportistaId);
            trip.Totalkilometros.Should().Be(10m + 20m);
            trip.Totalpagar.Should().Be(25m + 50m);
            trip.Usuariocrea.Should().Be(tripDto.Usuariocrea);
            trip.MonedaId.Should().Be(tripDto.MonedaId);
            trip.Viajesdetalles.Should().HaveCount(tripDetails.Count);
        }
    }
}
