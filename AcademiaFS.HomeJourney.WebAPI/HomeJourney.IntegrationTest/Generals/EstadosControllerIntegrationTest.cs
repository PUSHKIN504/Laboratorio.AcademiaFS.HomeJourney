using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcademiaFS.HomeJourney.WebAPI;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AcademiaFS.HomeJourney.WebAPI.Tests.Integration.Controllers
{
    public class EstadosControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EstadosControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOkAndListOfEstados()
        {
            // Act
            var response = await _client.GetAsync("/academiafarsiman/estados");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque se debe retornar el listado de estados");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<EstadoDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ExistingEstado_ReturnsEstado()
        {
            // Arrange
            var createDto = new EstadoDto
            {
                Nombre = "Estado de Prueba",
                Descripcion = "Descripción del nuevo estado"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/estados", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created, "porque se debe crear el estado correctamente");
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoDto>>();
            int estadoId = createCustomResponse.Data.EstadoId;

            // Act
            var getResponse = await _client.GetAsync($"/academiafarsiman/estados/{estadoId}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque el estado creado existe");
            var getCustomResponse = await getResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoDto>>();
            getCustomResponse.Should().NotBeNull();
            getCustomResponse.Success.Should().BeTrue();
            getCustomResponse.Data.Should().NotBeNull();
            getCustomResponse.Data.EstadoId.Should().Be(estadoId);
        }

        [Fact]
        public async Task Create_ValidDto_ReturnsCreatedEstado()
        {
            // Arrange
            var dto = new EstadoDto
            {
                Nombre = "Nuevo Estado",
                Descripcion = "Descripción del nuevo estado"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/estados", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque el estado debe crearse correctamente");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<EstadoDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Be("Estado creado correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.EstadoId.Should().BeGreaterThan(0);
            customResponse.Data.Nombre.Should().Be(dto.Nombre);
        }

        [Fact]
        public async Task Update_ValidDto_ReturnsUpdatedEstado()
        {
            // Arrange
            var createDto = new EstadoDto
            {
                Nombre = "Estado Original",
                Descripcion = "Descripción del nuevo estado"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/estados", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created, "porque el estado se crea correctamente");
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoDto>>();
            int estadoId = createCustomResponse.Data.EstadoId;

            var updateDto = new EstadoDto
            {
                EstadoId = estadoId,
                Nombre = "Estado Actualizado",
                Descripcion = "Descripción del estado actualizado"
            };

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/academiafarsiman/estados/{estadoId}", updateDto);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la actualización debe ser exitosa");
            var updateCustomResponse = await updateResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoDto>>();
            updateCustomResponse.Should().NotBeNull();
            updateCustomResponse.Success.Should().BeTrue();
            updateCustomResponse.Data.Should().NotBeNull();
            updateCustomResponse.Data.EstadoId.Should().Be(estadoId);
            updateCustomResponse.Data.Nombre.Should().Be("Estado Actualizado");
        }
    }
}
