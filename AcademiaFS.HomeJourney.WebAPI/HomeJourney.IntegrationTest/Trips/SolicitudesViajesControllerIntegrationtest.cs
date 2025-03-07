using System;
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
    public class SolicitudesViajesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SolicitudesViajesControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CrearSolicitudColaborador_ValidDto_ReturnsCreatedSolicitud()
        {
            // Arrange
            var solicitudCreateDto = new SolicitudViajeCreateDto
            {

                ColaboradorId = 1,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/academiafarsiman/solicitudesviajes/colaborador", solicitudCreateDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created, "porque se debe crear la solicitud de viaje para el colaborador");
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<Solicitudesviajes>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Contain("creada correctamente");
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.SolicitudviajeId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task AprobarSolicitud_ValidRequest_ReturnsOk()
        {
            // Arrange
            var solicitudCreateDto = new SolicitudViajeCreateDto
            {
                ColaboradorId = 1

            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/solicitudesviajes/colaborador", solicitudCreateDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<Solicitudesviajes>>();
            int solicitudId = createCustomResponse.Data.SolicitudviajeId;


            var aprobacionDto = new SolicitudViajeAprobacionDto
            {
                SolicitudviajeId = solicitudId,
                SupervisorId = 100,     
                Comentarios = "Aprobado",
                Aprobar = true           
            };

            // Act
            var response = await _client.PatchAsJsonAsync($"/academiafarsiman/solicitudesviajes/{solicitudId}/aprobar", aprobacionDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "porque la solicitud debe aprobarse correctamente");
            var customResponseAprobacion = await response.Content.ReadFromJsonAsync<CustomResponse<Solicitudesviajes>>();
            customResponseAprobacion.Should().NotBeNull();
            customResponseAprobacion.Success.Should().BeTrue();
            customResponseAprobacion.Message.Should().Contain("aprobada correctamente");
            customResponseAprobacion.Data.Should().NotBeNull();
            customResponseAprobacion.Data.SolicitudviajeId.Should().Be(solicitudId);

        }

        [Fact]
        public async Task CancelarSolicitud_ValidRequest_ReturnsOk()
        {
            // Arrange
            var solicitudCreateDto = new SolicitudViajeCreateDto
            {
                ColaboradorId = 1
 
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/solicitudesviajes/colaborador", solicitudCreateDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<Solicitudesviajes>>();
            int solicitudId = createCustomResponse.Data.SolicitudviajeId;

            var aprobacionDto = new SolicitudViajeAprobacionDto
            {
                SolicitudviajeId = solicitudId,
                SupervisorId = 100,
                Comentarios = "Aprobado para cancelar",
                Aprobar = true
            };
            var aprobacionResponse = await _client.PatchAsJsonAsync($"/academiafarsiman/solicitudesviajes/{solicitudId}/aprobar", aprobacionDto);
            aprobacionResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var cancelResponse = await _client.PatchAsync($"/academiafarsiman/solicitudesviajes/colaborador/{solicitudId}/cancelar?colaboradorId=1", null);

            // Assert
            cancelResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la solicitud debe cancelarse correctamente");
            var cancelCustomResponse = await cancelResponse.Content.ReadFromJsonAsync<CustomResponse<Solicitudesviajes>>();
            cancelCustomResponse.Should().NotBeNull();
            cancelCustomResponse.Success.Should().BeTrue();
            cancelCustomResponse.Message.Should().Contain("cancelada correctamente");
            cancelCustomResponse.Data.Should().NotBeNull();
            cancelCustomResponse.Data.SolicitudviajeId.Should().Be(solicitudId);

        }

        [Fact]
        public async Task GetById_ExistingSolicitud_ReturnsSolicitud()
        {
            // Arrange
            var solicitudCreateDto = new SolicitudViajeCreateDto
            {
                ColaboradorId = 1
            };

            var createResponse = await _client.PostAsJsonAsync("/academiafarsiman/solicitudesviajes/colaborador", solicitudCreateDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createCustomResponse = await createResponse.Content.ReadFromJsonAsync<CustomResponse<Solicitudesviajes>>();
            int solicitudId = createCustomResponse.Data.SolicitudviajeId;

            // Act
            var getResponse = await _client.GetAsync($"/academiafarsiman/solicitudesviajes/{solicitudId}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK, "porque la solicitud existe");
            var getCustomResponse = await getResponse.Content.ReadFromJsonAsync<CustomResponse<Solicitudesviajes>>();
            getCustomResponse.Should().NotBeNull();
            getCustomResponse.Success.Should().BeTrue();
            getCustomResponse.Data.Should().NotBeNull();
            getCustomResponse.Data.SolicitudviajeId.Should().Be(solicitudId);
        }
    }
}
