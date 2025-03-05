using System.Net;
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;
using AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto;
using AcademiaFS.HomeJourney.WebAPI.Utilities;

public class UsuariosControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsuariosControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsSuccessfulLogin()
    {
        var request = new UsuarioLoginRequest
        {
            Username = "testuser",
            Password = "testpassword"
        };

        var response = await _client.PostAsJsonAsync("/academiafarsiman/Usuarios/login", request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<CustomResponse<UsuarioConDetallesDto>>();
        content.Should().NotBeNull();
        content!.Success.Should().BeTrue();
        content.Message.Should().Be("Usuario autenticado correctamente");
        content.Data.Should().NotBeNull();

        var userData = content.Data!;
        userData.Username.Should().Be("testuser");
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var request = new UsuarioLoginRequest
        {
            Username = "testuser",
            Password = "wrongpassword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/academiafarsiman/Usuarios/login", request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response JSON: {content}"); 

        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<ErrorResponse>(content, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        errorResponse.Should().NotBeNull();
        errorResponse!.Message.Should().Be("Contraseña incorrecta."); 
    }

    [Fact]
    public async Task Login_NonExistentUser_ReturnsUnauthorized()
    {
        // Arrange
        var request = new UsuarioLoginRequest
        {
            Username = "nonexistentuser",
            Password = "anypassword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/academiafarsiman/Usuarios/login", request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response JSON: {content}"); 

        try
        {
            var errorResponse = System.Text.Json.JsonSerializer.Deserialize<ErrorResponse>(content, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            errorResponse.Should().NotBeNull();
            errorResponse!.Message.Should().Be("Usuario no encontrado o inactivo.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deserializando JSON: {content}", ex);
        }
    }






    private class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}
