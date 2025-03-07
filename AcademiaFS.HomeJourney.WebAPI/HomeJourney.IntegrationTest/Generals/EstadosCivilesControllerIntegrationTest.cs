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
    public class EstadosCivilesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EstadosCivilesControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOkAndListOfEstadosCiviles()
        {
            // Act
            var response = await _client.GetAsync("/academiafarsiman/estadosciviles");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque se debe retornar el listado de estados civiles");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<EstadoCivilDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ExistingEstadoCivil_ReturnsEstadoCivil()
        {
            // Arrange
            var createDto = new EstadoCivilDto
            {
                Nombre = "Soltero"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/estadosciviles", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created, "porque se creó el registro correctamente");
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoCivilDto>>();
            int id = createCustomResponse.Data.EstadocivilId;

            // Act
            var getResponse = await _client.GetAsync($"/academiafarsiman/estadosciviles/{id}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque el registro existe");
            var getCustomResponse = await getResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoCivilDto>>();
            getCustomResponse.Should().NotBeNull();
            getCustomResponse.Success.Should().BeTrue();
            getCustomResponse.Data.Should().NotBeNull();
            getCustomResponse.Data.EstadocivilId.Should().Be(id);
        }

        [Fact]
        public async Task Create_ValidDto_ReturnsCreatedEstadoCivil()
        {
            // Arrange
            var dto = new EstadoCivilDto
            {
                Nombre = "Casado"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/estadosciviles", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque el registro debe crearse correctamente");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<EstadoCivilDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Be("Estado civil creado correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.EstadocivilId.Should().BeGreaterThan(0);
            customResponse.Data.Nombre.Should().Be(dto.Nombre);
        }

        [Fact]
        public async Task Update_ValidDto_ReturnsUpdatedEstadoCivil()
        {
            // Arrange
            var createDto = new EstadoCivilDto
            {
                Nombre = "Divorciado"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/estadosciviles", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created, "porque el registro se crea correctamente");
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoCivilDto>>();
            int id = createCustomResponse.Data.EstadocivilId;
            var updateDto = new EstadoCivilDto
            {
                EstadocivilId = id,
                Nombre = "Viudo"
            };

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/academiafarsiman/estadosciviles/{id}", updateDto);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la actualización debe ser exitosa");
            var updateCustomResponse = await updateResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoCivilDto>>();
            updateCustomResponse.Should().NotBeNull();
            updateCustomResponse.Success.Should().BeTrue();
            updateCustomResponse.Data.Should().NotBeNull();
            updateCustomResponse.Data.EstadocivilId.Should().Be(id);
            updateCustomResponse.Data.Nombre.Should().Be("Viudo");
        }

        [Fact]
        public async Task SetActive_ValidRequest_UpdatesEstadoCivilActiveState()
        {
            // Arrange
            var createDto = new EstadoCivilDto
            {
                Nombre = "Unido"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/estadosciviles", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<EstadoCivilDto>>();
            int id = createCustomResponse.Data.EstadocivilId;

            // Act
            var patchResponseFalse = await _client.PatchAsync($"/academiafarsiman/estadosciviles/{id}?active=false", null);
            patchResponseFalse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la desactivación debe ser exitosa");
            var patchCustomResponseFalse = await patchResponseFalse.Content.ReadFromJsonAsync<CustomResponse<string>>();
            patchCustomResponseFalse.Should().NotBeNull();
            patchCustomResponseFalse.Success.Should().BeTrue();
            patchCustomResponseFalse.Message.Should().Contain("desactivado");
            var patchResponseTrue = await _client.PatchAsync($"/academiafarsiman/estadosciviles/{id}?active=true", null);
            patchResponseTrue.StatusCode.Should().Be(HttpStatusCode.OK, "porque la activación debe ser exitosa");
            var patchCustomResponseTrue = await patchResponseTrue.Content.ReadFromJsonAsync<CustomResponse<string>>();
            patchCustomResponseTrue.Should().NotBeNull();
            patchCustomResponseTrue.Success.Should().BeTrue();
            patchCustomResponseTrue.Message.Should().Contain("activado");
        }
    }
}
