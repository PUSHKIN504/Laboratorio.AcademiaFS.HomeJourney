using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcademiaFS.HomeJourney.WebAPI;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AcademiaFS.HomeJourney.WebAPI.Tests.Integration.Controllers
{
    public class ViajesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ViajesControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateViaje_ValidDto_ReturnsCreatedResponse()
        {
            // Arrange
            var dto = new ViajesCreateDto
            {
                Detalles = new List<ViajesdetallesCreateDto>
                {
                    new ViajesdetallesCreateDto
                    {
                        ColaboradorId = 1,
                        Distanciakilometros = 10.0m,
                        Totalpagar = 200.0m
                    }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/viajes", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque se creó correctamente el viaje y sus detalles");

            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<Viajes>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Be("Viaje y detalles creados correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.ViajeId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetById_ExistingViaje_ReturnsOkResponse()
        {
            // Arrange
            var createDto = new ViajesCreateDto
            {
                Detalles = new List<ViajesdetallesCreateDto>
        {
            new ViajesdetallesCreateDto
            {
                ColaboradorId = 1,
                Distanciakilometros = 10.0m,
                Totalpagar = 200.0m
            }
        }
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/viajes", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<Viajes>>();
            int viajeId = createCustomResponse.Data.ViajeId;

            // Act
            var getResponse = await _client.GetAsync($"/academiafarsiman/viajes/{viajeId}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque el viaje existe");
            var content = await getResponse.Content.ReadAsStringAsync();
            content.Should().BeEmpty("porque el endpoint GET no retorna ningún contenido");
        }

    }
}
