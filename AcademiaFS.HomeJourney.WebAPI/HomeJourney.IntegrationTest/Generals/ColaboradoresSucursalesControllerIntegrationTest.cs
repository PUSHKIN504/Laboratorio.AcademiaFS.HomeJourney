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
    public class ColaboradoresSucursalesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ColaboradoresSucursalesControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOkAndListOfRecords()
        {
            // Act
            var response = await _client.GetAsync("/academiafarsiman/colaboradoressucursales");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<ColaboradorSucursalDto>>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Message.Should().Contain("Listado");
            customResponse.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ExistingRecord_ReturnsRecord()
        {
            var allResponse = await _client.GetAsync("/academiafarsiman/colaboradoressucursales");
            allResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var allCustomResponse = await allResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<ColaboradorSucursalDto>>>();
            var record = allCustomResponse.Data.FirstOrDefault();
            record.Should().NotBeNull();

            // Act
            var response = await _client.GetAsync($"/academiafarsiman/colaboradoressucursales/{record.ColaboradorsucursalId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<ColaboradorSucursalDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.ColaboradorsucursalId.Should().Be(record.ColaboradorsucursalId);
        }

        [Fact]
        public async Task Create_ValidDto_ReturnsCreatedRecord()
        {
            var dto = new ColaboradorSucursalDto
            {
                ColaboradorId = 2,  
                SucursalId = 1   
            };

            var response = await _client.PostAsJsonAsync("/academiafarsiman/colaboradoressucursales", dto);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var customResponse = await response.Content.ReadFromJsonAsync<CustomResponse<ColaboradorSucursalDto>>();
            customResponse.Should().NotBeNull();
            customResponse.Success.Should().BeTrue();
            customResponse.Data.Should().NotBeNull();
            customResponse.Data.ColaboradorsucursalId.Should().BeGreaterThan(0);
        }


        [Fact]
        public async Task Update_ValidDto_ReturnsUpdatedRecord()
        {
            int id = 1;

            var updateDto = new ColaboradorSucursalDto
            {
                ColaboradorsucursalId = id,
                ColaboradorId = 1,
                SucursalId = 1
            };

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/academiafarsiman/colaboradoressucursales/{id}", updateDto);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var updateCustomResponse = await updateResponse.Content.ReadFromJsonAsync<CustomResponse<ColaboradorSucursalDto>>();
            updateCustomResponse.Should().NotBeNull();
            updateCustomResponse.Success.Should().BeTrue();
            updateCustomResponse.Data.Should().NotBeNull();
            updateCustomResponse.Data.ColaboradorsucursalId.Should().Be(id);
        }

        [Fact]
        public async Task SetActive_ValidRequest_UpdatesActiveState()
        {
            // Arrange
            int id = 1;

            // Act & Assert
            var patchResponseFalse = await _client.PatchAsync($"/academiafarsiman/colaboradoressucursales/{id}?active=false", null);
            patchResponseFalse.StatusCode.Should().Be(HttpStatusCode.OK);
            var patchCustomResponseFalse = await patchResponseFalse.Content.ReadFromJsonAsync<CustomResponse<ColaboradorSucursalDto>>();
            patchCustomResponseFalse.Should().NotBeNull();
            patchCustomResponseFalse.Success.Should().BeTrue();
            patchCustomResponseFalse.Message.Should().Be("El registro ha sido desactivado.");

            var patchResponseTrue = await _client.PatchAsync($"/academiafarsiman/colaboradoressucursales/{id}?active=true", null);
            patchResponseTrue.StatusCode.Should().Be(HttpStatusCode.OK);
            var patchCustomResponseTrue = await patchResponseTrue.Content.ReadFromJsonAsync<CustomResponse<ColaboradorSucursalDto>>();
            patchCustomResponseTrue.Should().NotBeNull();
            patchCustomResponseTrue.Success.Should().BeTrue();
            patchCustomResponseTrue.Message.Should().Be("El registro ha sido activado.");
        }
    }
}
