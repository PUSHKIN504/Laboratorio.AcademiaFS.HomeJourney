using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcademiaFS.HomeJourney.WebAPI;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AcademiaFS.HomeJourney.WebAPI.Tests.Integration.Controllers
{
    public class CiudadesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CiudadesControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOkAndListOfCiudades()
        {
            // Act
            var response = await _client.GetAsync("/academiafarsiman/ciudades");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque se debe retornar el listado de ciudades");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CiudadesDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateCity_ValidDto_ReturnsCreatedCity()
        {
            // Arrange
            var dto = new CiudadesDto
            {
                // Se asume que el DTO requiere al menos la propiedad 'Nombre'
                Nombre = "Ciudad de Prueba"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/ciudades", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque se debe crear la ciudad correctamente");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<CiudadesDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Be("Ciudad creada correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.CiudadId.Should().BeGreaterThan(0);
            customResponse.Data.Nombre.Should().Be(dto.Nombre);
        }

        [Fact]
        public async Task GetById_ExistingCity_ReturnsCity()
        {
            // Arrange: primero se crea una ciudad
            var createDto = new CiudadesDto
            {
                Nombre = "Ciudad para GetById"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/ciudades", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<CiudadesDto>>();
            int cityId = createCustomResponse.Data.CiudadId;

            // Act
            var getResponse = await _client.GetAsync($"/academiafarsiman/ciudades/{cityId}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la ciudad existe");
            var getCustomResponse = await getResponse.Content.ReadFromJsonAsync<CustomResponse<CiudadesDto>>();
            getCustomResponse.Should().NotBeNull();
            getCustomResponse.Success.Should().BeTrue();
            getCustomResponse.Data.Should().NotBeNull();
            getCustomResponse.Data.CiudadId.Should().Be(cityId);
        }

        [Fact]
        public async Task UpdateCity_ValidDto_ReturnsUpdatedCity()
        {
            // Arrange: crear una ciudad para actualizar
            var createDto = new CiudadesDto
            {
                Nombre = "Ciudad Original"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/ciudades", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<CiudadesDto>>();
            int cityId = createCustomResponse.Data.CiudadId;

            // Preparar el DTO para actualizar
            var updateDto = new CiudadesDto
            {
                CiudadId = cityId,
                Nombre = "Ciudad Actualizada"
            };

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/academiafarsiman/ciudades/{cityId}", updateDto);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la actualización debe ser exitosa");
            var updateCustomResponse = await updateResponse.Content.ReadFromJsonAsync<CustomResponse<CiudadesDto>>();
            updateCustomResponse.Should().NotBeNull();
            updateCustomResponse.Success.Should().BeTrue();
            updateCustomResponse.Data.Should().NotBeNull();
            updateCustomResponse.Data.CiudadId.Should().Be(cityId);
            updateCustomResponse.Data.Nombre.Should().Be("Ciudad Actualizada");
        }

        [Fact]
        public async Task SetActive_ValidRequest_UpdatesCityActiveState()
        {
            // Arrange: crear una ciudad para luego modificar su estado
            var createDto = new CiudadesDto
            {
                Nombre = "Ciudad para SetActive"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/ciudades", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<CiudadesDto>>();
            int cityId = createCustomResponse.Data.CiudadId;

            // Act: desactivar la ciudad
            var patchResponseFalse = await _client.PatchAsync($"/academiafarsiman/ciudades/{cityId}?active=false", null);
            patchResponseFalse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la desactivación debe ser exitosa");
            var patchCustomResponseFalse = await patchResponseFalse.Content.ReadFromJsonAsync<CustomResponse<CiudadesDto>>();
            patchCustomResponseFalse.Should().NotBeNull();
            patchCustomResponseFalse.Success.Should().BeTrue();
            patchCustomResponseFalse.Message.Should().Be("La ciudad ha sido desactivada");

            // Act: activar la ciudad
            var patchResponseTrue = await _client.PatchAsync($"/academiafarsiman/ciudades/{cityId}?active=true", null);
            patchResponseTrue.StatusCode.Should().Be(HttpStatusCode.OK, "porque la activación debe ser exitosa");
            var patchCustomResponseTrue = await patchResponseTrue.Content.ReadFromJsonAsync<CustomResponse<CiudadesDto>>();
            patchCustomResponseTrue.Should().NotBeNull();
            patchCustomResponseTrue.Success.Should().BeTrue();
            patchCustomResponseTrue.Message.Should().Be("La ciudad ha sido activada");
        }
    }
}
