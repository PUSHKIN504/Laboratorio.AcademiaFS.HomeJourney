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
    public class PaisesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PaisesControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOkAndListOfPaises()
        {
            // Act
            var response = await _client.GetAsync("/academiafarsiman/paises");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque se debe retornar el listado de países");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<PaisesDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Contain("Listado");
            customResponse.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ExistingPais_ReturnsPais()
        {
            // Arrange
            var createDto = new PaisesDto
            {
                Nombre = "País de Prueba",
                Activo = true
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/paises", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created, "porque se crea el país correctamente");
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<PaisesDto>>();
            int paisId = createCustomResponse.Data.PaisId;

            // Act
            var getResponse = await _client.GetAsync($"/academiafarsiman/paises/{paisId}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque el país existe");
            var getCustomResponse = await getResponse.Content.ReadFromJsonAsync<CustomResponse<PaisesDto>>();
            getCustomResponse.Should().NotBeNull();
            getCustomResponse.Success.Should().BeTrue();
            getCustomResponse.Data.Should().NotBeNull();
            getCustomResponse.Data.PaisId.Should().Be(paisId);
        }

        [Fact]
        public async Task Create_ValidDto_ReturnsCreatedPais()
        {
            // Arrange
            var dto = new PaisesDto
            {
                Nombre = "Nuevo País",
                Activo = true
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/paises", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque el país se crea correctamente");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<PaisesDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Be("País creado correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.PaisId.Should().BeGreaterThan(0);
            customResponse.Data.Nombre.Should().Be(dto.Nombre);
        }

        [Fact]
        public async Task Update_ValidDto_ReturnsUpdatedPais()
        {
            // Arrange
            var createDto = new PaisesDto
            {
                Nombre = "País Original",
                Activo = true
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/paises", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created, "porque el país se crea correctamente");
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<PaisesDto>>();
            int paisId = createCustomResponse.Data.PaisId;

            var updateDto = new PaisesDto
            {
                PaisId = paisId,
                Nombre = "País Actualizado",
                Activo = true
            };

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/academiafarsiman/paises/{paisId}", updateDto);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la actualización debe ser exitosa");
            var updateCustomResponse = await updateResponse.Content.ReadFromJsonAsync<CustomResponse<PaisesDto>>();
            updateCustomResponse.Should().NotBeNull();
            updateCustomResponse.Success.Should().BeTrue();
            updateCustomResponse.Data.Should().NotBeNull();
            updateCustomResponse.Data.PaisId.Should().Be(paisId);
            updateCustomResponse.Data.Nombre.Should().Be("País Actualizado");
        }

        [Fact]
        public async Task SetActive_ValidRequest_UpdatesPaisActiveState()
        {
            // Arrange
            var createDto = new PaisesDto
            {
                Nombre = "País para Activación",
                Activo = true
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/paises", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<PaisesDto>>();
            int paisId = createCustomResponse.Data.PaisId;

            // Act
            var patchResponseFalse = await _client.PatchAsync($"/academiafarsiman/paises/{paisId}?active=false", null);
            patchResponseFalse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la desactivación debe ser exitosa");
            var patchCustomResponseFalse = await patchResponseFalse.Content.ReadFromJsonAsync<CustomResponse<PaisesDto>>();
            patchCustomResponseFalse.Should().NotBeNull();
            patchCustomResponseFalse.Success.Should().BeTrue();
            patchCustomResponseFalse.Message.Should().Be("El país ha sido desactivado");
            var patchResponseTrue = await _client.PatchAsync($"/academiafarsiman/paises/{paisId}?active=true", null);
            patchResponseTrue.StatusCode.Should().Be(HttpStatusCode.OK, "porque la activación debe ser exitosa");
            var patchCustomResponseTrue = await patchResponseTrue.Content.ReadFromJsonAsync<CustomResponse<PaisesDto>>();
            patchCustomResponseTrue.Should().NotBeNull();
            patchCustomResponseTrue.Success.Should().BeTrue();
            patchCustomResponseTrue.Message.Should().Be("El país ha sido activado");
        }
    }
}
