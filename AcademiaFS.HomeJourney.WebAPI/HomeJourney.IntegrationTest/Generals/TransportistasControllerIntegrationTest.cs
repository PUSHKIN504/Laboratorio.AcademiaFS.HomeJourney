using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class TransportistasControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TransportistasControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_ValidDto_ReturnsCreatedTransportista()
        {
            // Arrange: Se crea el DTO con todos los campos requeridos.
            var createDto = new CreateTransportistaDto
            {
                Nombre = "Transportista de Prueba",
                Apelllido = "García",
                Sexo = "M",
                Email = "transporte@example.com",
                Documentonacionalidentificacion = "ABC123456",
                CiudadId = 1,            
                Usuariocrea = 1,        
                Tarifaporkilometro = 5.0m,
                ServiciotransporteId = 1, 
                MonedaId = null          
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/transportistas/crear", createDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque el transportista se debe crear correctamente");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<Transportistas>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Contain("creado correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.TransportistaId.Should().BeGreaterThan(0);
            customResponse.Data.Tarifaporkilometro.Should().Be(createDto.Tarifaporkilometro);
        }


        [Fact]
        public async Task GetById_ExistingTransportista_ReturnsTransportista()
        {
            // Arrange: Primero creamos un transportista
            var createDto = new CreateTransportistaDto
            {
                Nombre = "Transportista de Prueba",
                Apelllido = "García",
                Sexo = "M",
                Email = "transporte@example.com",
                Documentonacionalidentificacion = "ABC123456",
                CiudadId = 1,
                Usuariocrea = 1,
                Tarifaporkilometro = 5.0m,
                ServiciotransporteId = 1,
                MonedaId = null
            };


            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/transportistas/crear", createDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<Transportistas>>();
            int transportistaId = createCustomResponse.Data.TransportistaId;

            // Act: Consultar el transportista por su ID.
            var getResponse = await _client.GetAsync($"/academiafarsiman/transportistas/{transportistaId}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque el transportista existe");
            var getCustomResponse = await getResponse.Content.ReadFromJsonAsync<CustomResponse<Transportistas>>();
            getCustomResponse.Should().NotBeNull();
            getCustomResponse.Success.Should().BeTrue();
            getCustomResponse.Data.Should().NotBeNull();
            getCustomResponse.Data.TransportistaId.Should().Be(transportistaId);
        }

        [Fact]
        public async Task GetAllTransportistas_ReturnsOkAndList()
        {
            // Arrange: Se asume que en la semilla o mediante creación previa ya existen transportistas.
            // Si no es así, se puede crear uno.
            // Por ejemplo, creamos un transportista si la lista está vacía.
            var allResponseInitial = await _client.GetAsync("/academiafarsiman/transportistas");
            var initialResponse = await allResponseInitial.Content.ReadFromJsonAsync<CustomResponse<List<TransportistaGetAllDto>>>();
            if (initialResponse.Data == null || !initialResponse.Data.Any())
            {
                var createDto = new CreateTransportistaDto
                {
                    Nombre = "Transportista para GetAll",
                    Tarifaporkilometro = 6.0m
                };
                await _client.PostAsJsonAsync("/academiafarsiman/transportistas/crear", createDto);
            }

            // Act: Llamar al endpoint para obtener la lista de transportistas.
            var response = await _client.GetAsync("/academiafarsiman/transportistas");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque se debe retornar la lista de transportistas");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<List<TransportistaGetAllDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.Any().Should().BeTrue("porque debe existir al menos un transportista");
        }
    }
}
