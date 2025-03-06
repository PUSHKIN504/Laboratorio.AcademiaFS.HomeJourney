using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcademiaFS.HomeJourney.WebAPI;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AcademiaFS.HomeJourney.WebAPI.Tests.Integration.Controllers
{
    public class ViajesClusteredControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ViajesClusteredControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ClusterEmployees_ValidRequest_ReturnsOk()
        {
            // Arrange
            int usuarioCrea = 1;
            decimal distanceThreshold = 10.5m;

            var employees = new List<ViajesdetallesCreateClusteredDto>
            {
                new ViajesdetallesCreateClusteredDto
                {
                    ColaboradorId = 2,
                    Distanciakilometros = 5.0m,
                    Totalpagar = 100.0m,
                    ColaboradorsucursalId = 1, 
                    MonedaId = 1,
                    Latitud = 19.432608,
                    Longitud = -99.133209
                },
                new ViajesdetallesCreateClusteredDto
                {
                    ColaboradorId = 3,
                    Distanciakilometros = 7.5m,
                    Totalpagar = 150.0m,
                    ColaboradorsucursalId = 1,
                    MonedaId = 1,
                    Latitud = 19.432700,
                    Longitud = -99.133300
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                $"/api/viajesclustered/cluster-employees?usuarioCrea={usuarioCrea}&distanceThreshold={distanceThreshold}",
                employees
            );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque la operación de clustering debe completarse exitosamente");
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<object>>();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetReporteViajesPorTransportista_ValidRequest_ReturnsOk()
        {
            // Arrange
            int usuarioCrea = 1;
            DateTime fechaInicio = DateTime.Today.AddDays(-7);
            DateTime fechaFin = DateTime.Today;
            int? transportistaId = 10; 

            var url = $"/api/viajesclustered/reporte-viajes-transportista?usuarioCrea={usuarioCrea}" +
                      $"&fechaInicio={fechaInicio:yyyy-MM-dd}" +
                      $"&fechaFin={fechaFin:yyyy-MM-dd}" +
                      $"&transportistaId={transportistaId}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque se debe generar el reporte sin inconvenientes");
            var reportes = await response.Content.ReadFromJsonAsync<IEnumerable<object>>();
            reportes.Should().NotBeNull();
        }
        [Fact]
        public async Task CreateTripsFromClusters_ValidRequest_ReturnsOk()
        {
            // Arrange
            int usuarioCrea = 1; 

            var viajeClusteredDto = new ViajesCreateClusteredDto
            {
                SucursalId = 1,             
                TransportistaIds = new List<int> { 10 },
                EstadoId = 1,                 
                Viajehora = new TimeSpan(8, 0, 0), 
                Viajefecha = DateTime.Today,     
                Usuariocrea = usuarioCrea,
                MonedaId = 1
            };

            var empleadosClusteredDto = new List<List<ViajesdetallesCreateClusteredDto>>
            {
                new List<ViajesdetallesCreateClusteredDto>
                {
                    new ViajesdetallesCreateClusteredDto
                    {
                        ColaboradorId = 2,
                        Distanciakilometros = 5.0m,
                        Totalpagar = 100.0m,
                        ColaboradorsucursalId = 1,
                        MonedaId = 1,
                        Latitud = 19.432608,
                        Longitud = -99.133209
                    },
                    new ViajesdetallesCreateClusteredDto
                    {
                        ColaboradorId = 3,
                        Distanciakilometros = 6.5m,
                        Totalpagar = 120.0m,
                        ColaboradorsucursalId = 1,
                        MonedaId = 1,
                        Latitud = 19.432700,
                        Longitud = -99.133300
                    }
                }
            };

            var request = new CreateViajesRequest
            {
                viajeclusteredDto = viajeClusteredDto,
                empleadosclusteredDto = empleadosClusteredDto
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                $"/api/viajesclustered/create-trips-from-clusters?usuarioCrea={usuarioCrea}",
                request
            );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque la creación de viajes a partir de clusters debe completarse sin problemas");
            var trips = await response.Content.ReadFromJsonAsync<IEnumerable<dynamic>>();
            trips.Should().NotBeNull();
            trips.Any().Should().BeTrue("porque se debe retornar al menos un viaje creado");
        }
    }
}
