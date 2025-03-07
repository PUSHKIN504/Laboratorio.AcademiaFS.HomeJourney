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
    public class PersonasColaboradoresControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PersonasColaboradoresControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_ValidDto_ReturnsCreatedPersonaColaborador()
        {
            // Arrange
            var createDto = new CreatePersonaColaboradorDto
            {
                Nombre = "Juan",
                Apelllido = "Pérez",
                Documentonacionalidentificacion = "12345678",
                Email = "juan.perez@example.com",
                Sexo = "M",
                Activo = true,
                EstadocivilId = 1,
                CiudadId = 1,
                Usuariocrea = 2,
                RolId = 1,
                CargoId = 1,
                Direccion = "Calle 123",
                Latitud = 0.0m,
                Longitud = 0.0m
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/personascolaboradores/crear", createDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque se debe crear la persona y colaborador correctamente");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<Personas>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Contain("creados correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.PersonaId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetById_ExistingPersona_ReturnsPersona()
        {
            // Arrange
            var createDto = new CreatePersonaColaboradorDto
            {
                Nombre = "María",
                Apelllido = "López",
                Documentonacionalidentificacion = "87654321",
                Email = "maria.lopez@example.com",
                Sexo = "F",
                Activo = true,
                EstadocivilId = 1,
                CiudadId = 1,
                Usuariocrea = 2,
                RolId = 1,
                CargoId = 1,
                Direccion = "Calle 123",
                Latitud = 0.0m,
                Longitud = 0.0m
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/personascolaboradores/crear", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<Personas>>();
            int personaId = createCustomResponse.Data.PersonaId;

            // Act
            var getResponse = await _client.GetAsync($"/academiafarsiman/personascolaboradores/{personaId}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var getCustomResponse = await getResponse.Content.ReadFromJsonAsync<CustomResponse<PersonaDto>>();
            getCustomResponse.Should().NotBeNull();
            getCustomResponse.Success.Should().BeTrue();
            getCustomResponse.Data.Should().NotBeNull();
            getCustomResponse.Data.PersonaId.Should().Be(personaId);
        }

        [Fact]
        public async Task GetAllColaboradores_ReturnsOkAndList()
        {

            // Act
            var response = await _client.GetAsync("/academiafarsiman/personascolaboradores");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<List<ColaboradorGetAllDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.Any().Should().BeTrue("porque debe haber al menos un colaborador en la base de datos");
        }
    }
}
