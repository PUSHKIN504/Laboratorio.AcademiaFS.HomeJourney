namespace AcademiaFS.HomeJourney.WebAPI._Common
{
    using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
    using System.Text.Json;

    public interface IGoogleMapsService
    {
        Task<double[,]> GetDistanceMatrixAsync(List<ViajesdetallesCreateClusteredDto> employees);
    }

    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _googleApiKey;

        public GoogleMapsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _googleApiKey = configuration["GoogleApiKey:ApiKey"] ?? throw new InvalidOperationException("Google API Key no configurada.");
        }

        public async Task<double[,]> GetDistanceMatrixAsync(List<ViajesdetallesCreateClusteredDto> employees)
        {
            if (employees == null || !employees.Any())
                throw new ArgumentException("La lista de empleados no puede ser nula o vacía.");

            var locations = employees.Select(e => $"{e.Latitud},{e.Longitud}").ToList();
            var origins = Uri.EscapeDataString(string.Join("|", locations));
            var destinations = Uri.EscapeDataString(string.Join("|", locations));
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origins}&destinations={destinations}&units=metric&key={_googleApiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DistanceMatrixResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result?.Status != "OK")
                    throw new Exception($"Error en la API de Google: {result?.Status ?? "Error desconocido"}");

                var matrix = BuildDistanceMatrix(result, employees.Count);
                return matrix;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Fallo al conectar con Google Maps API: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Fallo al parsear la respuesta de Google API: {ex.Message}", ex);
            }
        }

        private double[,] BuildDistanceMatrix(DistanceMatrixResponse result, int employeeCount)
        {
            if (result.Rows.Length != employeeCount)
                throw new Exception("La cantidad de filas no coincide con la cantidad de empleados.");

            var matrix = new double[employeeCount, employeeCount];
            for (int i = 0; i < employeeCount; i++)
            {
                var elements = result.Rows[i].Elements;
                if (elements.Length != employeeCount)
                    throw new Exception($"Cantidad de elementos inválida en la fila {i}.");

                for (int j = 0; j < employeeCount; j++)
                {
                    if (elements[j].Status != "OK")
                        throw new Exception($"Fallo al calcular la distancia entre empleados en posición {i} y {j}.");
                    matrix[i, j] = elements[j].Distance.Value / 1000.0;
                }
            }
            return matrix;
        }
    }


    public class DistanceMatrixResponse
    {
        public string Status { get; set; }
        public Row[] Rows { get; set; }
    }

    public class Row
    {
        public Element[] Elements { get; set; }
    }

    public class Element
    {
        public string Status { get; set; }
        public Distance Distance { get; set; }
    }

    public class Distance
    {
        public double Value { get; set; }
    }
}
