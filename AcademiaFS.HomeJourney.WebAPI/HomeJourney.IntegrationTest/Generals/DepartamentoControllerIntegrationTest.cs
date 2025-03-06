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
    public class DepartamentosControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public DepartamentosControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOkAndListOfDepartamentos()
        {
            // Act
            var response = await _client.GetAsync("/academiafarsiman/departamentos");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque se debe retornar el listado de departamentos");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<DepartamentoDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Contain("Listado");
            customResponse.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ExistingDepartamento_ReturnsDepartamento()
        {
            // Arrange: Primero se crea un departamento para luego obtenerlo.
            var createDto = new DepartamentoDto
            {
                // Asigna las propiedades requeridas; por ejemplo, solo el Nombre
                Nombre = "Departamento de Prueba"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/departamentos", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<DepartamentoDto>>();
            int deptId = createCustomResponse.Data.DepartamentoId;

            // Act
            var getResponse = await _client.GetAsync($"/academiafarsiman/departamentos/{deptId}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque el departamento existe");
            var getCustomResponse = await getResponse.Content.ReadFromJsonAsync<CustomResponse<DepartamentoDto>>();
            getCustomResponse.Should().NotBeNull();
            getCustomResponse.Success.Should().BeTrue();
            getCustomResponse.Data.Should().NotBeNull();
            getCustomResponse.Data.DepartamentoId.Should().Be(deptId);
        }

        [Fact]
        public async Task Create_ValidDto_ReturnsCreatedDepartamento()
        {
            // Arrange: Construir un DTO con los datos mínimos requeridos.
            var dto = new DepartamentoDto
            {
                // Asegúrate de enviar los datos necesarios según tu mapeo. Por ejemplo:
                Nombre = "Nuevo Departamento"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/departamentos", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque se debe crear el departamento correctamente");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<DepartamentoDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Be("Departamento creado correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.DepartamentoId.Should().BeGreaterThan(0);
            customResponse.Data.Nombre.Should().Be(dto.Nombre);
        }

        [Fact]
        public async Task Update_ValidDto_ReturnsUpdatedDepartamento()
        {
            // Arrange: Crear un departamento primero.
            var createDto = new DepartamentoDto
            {
                Nombre = "Departamento Original"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/departamentos", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<DepartamentoDto>>();
            int deptId = createCustomResponse.Data.DepartamentoId;

            // Preparar el DTO para la actualización (manteniendo el mismo ID)
            var updateDto = new DepartamentoDto
            {
                DepartamentoId = deptId,
                Nombre = "Departamento Actualizado"
            };

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/academiafarsiman/departamentos/{deptId}", updateDto);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la actualización debe ser exitosa");
            var updateCustomResponse = await updateResponse.Content.ReadFromJsonAsync<CustomResponse<DepartamentoDto>>();
            updateCustomResponse.Should().NotBeNull();
            updateCustomResponse.Success.Should().BeTrue();
            updateCustomResponse.Data.Should().NotBeNull();
            updateCustomResponse.Data.DepartamentoId.Should().Be(deptId);
            updateCustomResponse.Data.Nombre.Should().Be("Departamento Actualizado");
        }

        [Fact]
        public async Task SetActive_ValidRequest_UpdatesDepartamentoActiveState()
        {
            // Arrange: Crear un departamento para modificar su estado.
            var createDto = new DepartamentoDto
            {
                Nombre = "Departamento para Activación"
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/departamentos", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<DepartamentoDto>>();
            int deptId = createCustomResponse.Data.DepartamentoId;

            // Act: Desactivar el departamento
            var patchResponseFalse = await _client.PatchAsync($"/academiafarsiman/departamentos/{deptId}?active=false", null);
            patchResponseFalse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la desactivación debe ser exitosa");
            var patchCustomResponseFalse = await patchResponseFalse.Content.ReadFromJsonAsync<CustomResponse<DepartamentoDto>>();
            patchCustomResponseFalse.Should().NotBeNull();
            patchCustomResponseFalse.Success.Should().BeTrue();
            patchCustomResponseFalse.Message.Should().Be("El departamento ha sido desactivado");

            // Act: Activar el departamento
            var patchResponseTrue = await _client.PatchAsync($"/academiafarsiman/departamentos/{deptId}?active=true", null);
            patchResponseTrue.StatusCode.Should().Be(HttpStatusCode.OK, "porque la activación debe ser exitosa");
            var patchCustomResponseTrue = await patchResponseTrue.Content.ReadFromJsonAsync<CustomResponse<DepartamentoDto>>();
            patchCustomResponseTrue.Should().NotBeNull();
            patchCustomResponseTrue.Success.Should().BeTrue();
            patchCustomResponseTrue.Message.Should().Be("El departamento ha sido activado");
        }
    }
}
