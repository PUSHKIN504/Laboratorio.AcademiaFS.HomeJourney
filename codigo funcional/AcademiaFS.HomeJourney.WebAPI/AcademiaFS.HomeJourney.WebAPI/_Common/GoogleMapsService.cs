using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using System.Text.Json;

namespace AcademiaFS.HomeJourney.WebAPI._Common
{
    public class GoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _googleApiKey;

        public GoogleMapsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _googleApiKey = configuration["GoogleApiKey:ApiKey"];
        }

        /// <summary>
        /// Devuelve una matriz de distancias (en kilómetros) entre todos los puntos proporcionados.
        /// </summary>
        /// <param name="locations">Lista de cadenas "lat,lon".</param>
        /// <returns>Matriz NxN con las distancias en km.</returns>
        /// <exception cref="InvalidOperationException">Si no hay API Key configurada.</exception>
        /// <exception cref="Exception">Si la respuesta de Google es inválida o no exitosa.</exception>
        public async Task<double[,]> GetDistanceMatrixAsync(List<string> locations)
        {
            if (string.IsNullOrEmpty(_googleApiKey))
                throw new InvalidOperationException("Google API Key no está configurada.");

            if (locations == null || !locations.Any())
                throw new ArgumentException("La lista de ubicaciones no puede ser nula o vacía.");

            var origins = Uri.EscapeDataString(string.Join("|", locations));
            var destinations = Uri.EscapeDataString(string.Join("|", locations));

            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origins}&destinations={destinations}&units=metric&key={_googleApiKey}";

            try
            {
                Console.WriteLine($"Requesting URL: {url}");
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response JSON: {json}");

                var result = JsonSerializer.Deserialize<DistanceMatrixResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result == null || result.Status != "OK")
                    throw new Exception($"Google API error: {result?.Status ?? "Unknown error"}");

                if (result.Rows == null || result.Rows.Length != locations.Count)
                    throw new Exception("Respuesta inválida: la cantidad de filas no coincide.");

                // Llenar la matriz
                var matrix = new double[locations.Count, locations.Count];
                for (int i = 0; i < locations.Count; i++)
                {
                    var elements = result.Rows[i].Elements;
                    if (elements.Length != locations.Count)
                        throw new Exception($"Respuesta inválida: la cantidad de columnas no coincide en la fila {i}.");

                    for (int j = 0; j < locations.Count; j++)
                    {
                        if (elements[j].Status != "OK")
                            throw new Exception($"No se pudo calcular la distancia entre {locations[i]} y {locations[j]}.");

                        // La distancia viene en metros; se convierte a km
                        matrix[i, j] = elements[j].Distance.Value / 1000.0;
                    }
                }
                return matrix;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"No se pudo conectar con la API de Google Maps: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"No se pudo deserializar la respuesta de la API de Google: {ex.Message}", ex);
            }
        }
    }
}
