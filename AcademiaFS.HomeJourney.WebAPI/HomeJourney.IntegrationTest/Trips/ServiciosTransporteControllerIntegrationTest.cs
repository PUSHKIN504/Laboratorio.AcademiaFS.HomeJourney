using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcademiaFS.HomeJourney.WebAPI;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AcademiaFS.HomeJourney.WebAPI.Tests.Integration.Controllers
{
    public class ServiciostransportesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ServiciostransportesControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOkAndListOfServicios()
        {
            // Act
            var response = await _client.GetAsync("/academiafarsiman/serviciostransportes");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<ServicioTransporteDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue("porque se debe obtener la lista de servicios");
            customResponse.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ExistingService_ReturnsOkAndService()
        {
            var nuevoServicio = new ServicioTransporteDto
            {
                Nombre = "Servicio Test",
                Descripcion = "Descripción del servicio",
                Email = "test@example.com",
                Activo = true,
                Fechacrea = DateTime.UtcNow,
                Usuariocrea = 1,
            };

            var postResponse = await _client.PostAsJsonAsync("/academiafarsiman/serviciostransportes", nuevoServicio);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Act
            var response = await _client.GetAsync("/academiafarsiman/serviciostransportes/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<ServicioTransporteDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.ServiciotransporteId.Should().Be(1);
        }

        [Fact]
        public async Task GetById_NonExistingService_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("/academiafarsiman/serviciostransportes/9999");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<ServicioTransporteDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeFalse();
            customResponse.Message.Should().Contain("No se encontró el servicio con ID");
        }

        [Fact]
        public async Task Create_Service_ReturnsCreatedAndService()
        {
            // Arrange
            var nuevoServicio = new ServicioTransporteDto
            {
                Nombre = "Servicio Creado",
                Descripcion = "Descripción Creada",
                Email = "creado@example.com",
                Activo = true,
                Fechacrea = DateTime.UtcNow,
                Usuariocrea= 1,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/serviciostransportes", nuevoServicio);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<ServicioTransporteDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.Nombre.Should().Be("Servicio Creado");
        }

        [Fact]
        public async Task Update_Service_ReturnsOkAndUpdatedService()
        {
            var nuevoServicio = new ServicioTransporteDto
            {
                Nombre = "Servicio a Actualizar",
                Descripcion = "Descripción Original",
                Email = "original@example.com",
                Activo = true,
                Fechacrea = DateTime.UtcNow,
                Usuariocrea = 1,
            };

            var postResponse = await _client.PostAsJsonAsync("/academiafarsiman/serviciostransportes", nuevoServicio);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdResponse = await postResponse.Content.ReadFromJsonAsync<CustomResponse<ServicioTransporteDto>>();
            int id = createdResponse.Data.ServiciotransporteId;

            var servicioActualizado = new Serviciostransportes
            {
                ServiciotransporteId = id,
                Nombre = "Servicio Actualizado",
                Descripcion = "Descripción Actualizada",
                Email = "actualizado@example.com",
                Activo = true,
                Fechacrea = DateTime.UtcNow,
                Usuariocrea = 1,
            };

            // Act
            var putResponse = await _client.PutAsJsonAsync($"/academiafarsiman/serviciostransportes/{id}", servicioActualizado);
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var customResponse = await putResponse.Content.ReadFromJsonAsync<CustomResponse<ServicioTransporteDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.Nombre.Should().Be("Servicio Actualizado");
            customResponse.Data.Descripcion.Should().Be("Descripción Actualizada");
            customResponse.Data.Email.Should().Be("actualizado@example.com");
        }

        [Fact]
        public async Task SetActive_Service_ReturnsOkAndTogglesActiveState()
        {
            var nuevoServicio = new ServicioTransporteDto
            {
                Nombre = "Servicio para SetActive",
                Descripcion = "Descripción SetActive",
                Email = "setactive@example.com",
                Activo = true,
                Fechacrea = DateTime.UtcNow,
                Usuariocrea = 1,
            };

            var postResponse = await _client.PostAsJsonAsync("/academiafarsiman/serviciostransportes", nuevoServicio);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdResponse = await postResponse.Content.ReadFromJsonAsync<CustomResponse<ServicioTransporteDto>>();
            int id = createdResponse.Data.ServiciotransporteId;

            // Act
            var patchResponse = await _client.PatchAsync($"/academiafarsiman/serviciostransportes/{id}?active=false", null);
            patchResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var customResponse = await patchResponse.Content.ReadFromJsonAsync<CustomResponse<ServicioTransporteDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.Activo.Should().BeFalse("porque se actualizó para desactivar el servicio");
        }
    }
}
