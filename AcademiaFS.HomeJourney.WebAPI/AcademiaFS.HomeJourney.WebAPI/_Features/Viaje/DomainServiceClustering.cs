using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using System.Text.Json;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class DomainServiceClustering
    {
        //private readonly HttpClient _httpClient;
        //private readonly string _googleApiKey;
        //private readonly Random _random = new Random();

        //public DomainServiceClustering(HttpClient httpClient, IConfiguration configuration)
        //{
        //    _httpClient = httpClient;
        //    _googleApiKey = configuration["GoogleApiKey:ApiKey"];
        //}

        //public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
        //    List<ViajesdetallesCreateClusteredDto> employees, decimal umbraldistancia, string origin)
        //{
        //    if (employees == null || !employees.Any())
        //        throw new ArgumentException("Employee list cannot be null or empty.");
        //    if (umbraldistancia <= 0)
        //        throw new ArgumentException("Distance threshold must be positive.");
        //    if (string.IsNullOrEmpty(origin))
        //        throw new ArgumentException("Origin address or coordinates must be provided.");

        //    var distances = await GetRealDistancesAsync(employees, origin);
        //    int k = Math.Max(1, (int)Math.Ceiling((distances.Max() - distances.Min()) / (double)umbraldistancia));
        //    var clusterAssignments = KMeansCluster(distances, k);

        //    var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>();
        //    for (int i = 0; i < k; i++)
        //    {
        //        var cluster = employees
        //            .Where((e, idx) => clusterAssignments[idx] == i)
        //            .ToList();
        //        if (cluster.Any())
        //            clusteredEmployees.Add(cluster);
        //    }

        //    return clusteredEmployees;
        //}

        //public List<Viajes> CreateTripsFromClusters(
        //    ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
        //{
        //    if (tripDto == null)
        //        throw new ArgumentNullException(nameof(tripDto));
        //    if (tripDto.SucursalId <= 0 || tripDto.EstadoId <= 0)
        //        throw new ArgumentException("SucursalId and EstadoId must be positive.");
        //    if (clusteredEmployees == null || !clusteredEmployees.Any())
        //        throw new ArgumentException("Clustered employees cannot be null or empty.");
        //    if (tripDto.TransportistaIds == null || tripDto.TransportistaIds.Count < clusteredEmployees.Count)
        //        throw new ArgumentException("The number of transportistas must match or exceed the number of clusters.");

        //    var trips = new List<Viajes>();
        //    var availableTransportistas = new List<int>(tripDto.TransportistaIds);

        //    foreach (var cluster in clusteredEmployees)
        //    {
        //        var tripDetails = cluster.Select(d => new Viajesdetalles
        //        {
        //            ColaboradorId = d.ColaboradorId,
        //            Distanciakilometros = d.Distanciakilometros,
        //            Totalpagar = d.Totalpagar,
        //            ColaboradorsucursalId = d.ColaboradorsucursalId,
        //            Activo = true,
        //            Usuariocrea = tripDto.Usuariocrea,
        //            Fechacrea = DateTime.Now,
        //            MonedaId = d.MonedaId
        //        }).ToList();

        //        int randomIndex = _random.Next(0, availableTransportistas.Count);
        //        int selectedTransportistaId = availableTransportistas[randomIndex];
        //        availableTransportistas.RemoveAt(randomIndex);

        //        var trip = new Viajes
        //        {
        //            SucursalId = tripDto.SucursalId,
        //            TransportistaId = selectedTransportistaId,
        //            EstadoId = tripDto.EstadoId,
        //            Viajehora = tripDto.Viajehora,
        //            Viajefecha = tripDto.Viajefecha,
        //            Totalkilometros = tripDetails.Sum(d => d.Distanciakilometros),
        //            Totalpagar = tripDetails.Sum(d => d.Totalpagar),
        //            Activo = true,
        //            Usuariocrea = tripDto.Usuariocrea,
        //            Fechacrea = DateTime.Now,
        //            MonedaId = tripDto.MonedaId,
        //            Viajesdetalles = tripDetails
        //        };

        //        trips.Add(trip);
        //    }

        //    return trips;
        //}

        //private async Task<List<double>> GetRealDistancesAsync(List<ViajesdetallesCreateClusteredDto> employees, string origin)
        //{
        //    var destinations = employees.Select(e => $"{e.Latitud},{e.Longitud}").ToList();
        //    var url = $"https://maps.googleapis.com/maps/api/distancematrix/json" +
        //              $"?origins={Uri.EscapeDataString(origin)}" +
        //              $"&destinations={Uri.EscapeDataString(string.Join("|", destinations))}" +
        //              $"&units=metric&key={_googleApiKey}";

        //    var response = await _httpClient.GetAsync(url);
        //    response.EnsureSuccessStatusCode();

        //    var json = await response.Content.ReadAsStringAsync();
        //    var result = JsonSerializer.Deserialize<DistanceMatrixResponse>(json, new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    });

        //    if (result.Status != "OK")
        //        throw new Exception($"Google API error: {result.Status}");

        //    return result.Rows[0].Elements
        //        .Select(e => e.Status == "OK" ? e.Distance.Value / 1000.0 : double.MaxValue)
        //        .ToList();
        //}

        //private List<int> KMeansCluster(List<double> data, int k)
        //{
        //    if (data.Count < k) return Enumerable.Range(0, data.Count).ToList();

        //    var random = new Random();
        //    var centroids = data.OrderBy(x => random.Next()).Take(k).ToList();
        //    var assignments = new List<int>(new int[data.Count]);
        //    bool changed;

        //    do
        //    {
        //        changed = false;
        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            int nearestCentroid = 0;
        //            double minDistance = Math.Abs(data[i] - centroids[0]);
        //            for (int j = 1; j < k; j++)
        //            {
        //                double distance = Math.Abs(data[i] - centroids[j]);
        //                if (distance < minDistance)
        //                {
        //                    minDistance = distance;
        //                    nearestCentroid = j;
        //                }
        //            }
        //            if (assignments[i] != nearestCentroid)
        //            {
        //                assignments[i] = nearestCentroid;
        //                changed = true;
        //            }
        //        }
        //        for (int j = 0; j < k; j++)
        //        {
        //            var clusterPoints = data.Where((_, idx) => assignments[idx] == j).ToList();
        //            if (clusterPoints.Count > 0)
        //                centroids[j] = clusterPoints.Average();
        //        }
        //    } while (changed);

        //    return assignments;
        //}
        private readonly HttpClient _httpClient;
        private readonly string _googleApiKey;
        private readonly Random _random = new Random();

        public DomainServiceClustering(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _googleApiKey = configuration["GoogleApiKey:ApiKey"];
        }

        public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
            List<ViajesdetallesCreateClusteredDto> employees, decimal distanceThreshold)
        {
            if (employees == null || !employees.Any())
                throw new ArgumentException("Employee list cannot be null or empty.");
            if (distanceThreshold <= 0)
                throw new ArgumentException("Distance threshold must be positive.");

            // Obtener la matriz de distancias entre todos los empleados
            var distanceMatrix = await GetDistanceMatrixAsync(employees);

            // Aplicar clustering jerárquico basado en el umbral
            var clusters = HierarchicalClustering(distanceMatrix, (double)distanceThreshold);

            // Mapear los clusters a la lista de empleados
            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>();
            foreach (var cluster in clusters)
            {
                var group = employees.Where((_, idx) => cluster.Contains(idx)).ToList();
                if (group.Any())
                    clusteredEmployees.Add(group);
            }

            return clusteredEmployees;
        }

        public List<Viajes> CreateTripsFromClusters(
            ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
        {
            if (tripDto == null) throw new ArgumentNullException(nameof(tripDto));
            if (tripDto.SucursalId <= 0 || tripDto.EstadoId <= 0) throw new ArgumentException("Invalid trip data.");
            if (clusteredEmployees == null || !clusteredEmployees.Any()) throw new ArgumentException("Clustered employees cannot be empty.");
            if (tripDto.TransportistaIds == null || tripDto.TransportistaIds.Count < clusteredEmployees.Count)
                throw new ArgumentException("Not enough transportistas for the clusters.");

            var trips = new List<Viajes>();
            var availableTransportistas = new List<int>(tripDto.TransportistaIds);

            foreach (var cluster in clusteredEmployees)
            {
                var tripDetails = cluster.Select(d => new Viajesdetalles
                {
                    ColaboradorId = d.ColaboradorId,
                    Distanciakilometros = d.Distanciakilometros,
                    Totalpagar = d.Totalpagar,
                    ColaboradorsucursalId = d.ColaboradorsucursalId,
                    Activo = true,
                    Usuariocrea = tripDto.Usuariocrea,
                    Fechacrea = DateTime.Now,
                    MonedaId = d.MonedaId
                }).ToList();

                int randomIndex = _random.Next(0, availableTransportistas.Count);
                int transportistaId = availableTransportistas[randomIndex];
                availableTransportistas.RemoveAt(randomIndex);

                trips.Add(new Viajes
                {
                    SucursalId = tripDto.SucursalId,
                    TransportistaId = transportistaId,
                    EstadoId = tripDto.EstadoId,
                    Viajehora = tripDto.Viajehora,
                    Viajefecha = tripDto.Viajefecha,
                    Totalkilometros = tripDetails.Sum(d => d.Distanciakilometros),
                    Totalpagar = tripDetails.Sum(d => d.Totalpagar),
                    Activo = true,
                    Usuariocrea = tripDto.Usuariocrea,
                    Fechacrea = DateTime.Now,
                    MonedaId = tripDto.MonedaId,
                    Viajesdetalles = tripDetails
                });
            }
            return trips;
        }

        private async Task<double[,]> GetDistanceMatrixAsync(List<ViajesdetallesCreateClusteredDto> employees)
        {
            if (string.IsNullOrEmpty(_googleApiKey))
                throw new InvalidOperationException("Google API Key is not configured.");
            if (employees == null || !employees.Any())
                throw new ArgumentException("Employee list cannot be null or empty.");

            var locations = employees.Select(e => $"{e.Latitud},{e.Longitud}").ToList();
            var origins = Uri.EscapeDataString(string.Join("|", locations));
            var destinations = Uri.EscapeDataString(string.Join("|", locations));
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origins}&destinations={destinations}&units=metric&key={_googleApiKey}";

            try
            {
                Console.WriteLine($"Requesting URL: {url}"); // Para depuración
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response JSON: {json}"); // Para depuración
                var result = JsonSerializer.Deserialize<DistanceMatrixResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result == null || result.Status != "OK")
                    throw new Exception($"Google API error: {result?.Status ?? "Unknown error"}");

                if (result.Rows == null || result.Rows.Length != employees.Count)
                    throw new Exception("Invalid response: Row count does not match employee count.");

                var matrix = new double[employees.Count, employees.Count];
                for (int i = 0; i < employees.Count; i++)
                {
                    var elements = result.Rows[i].Elements;
                    if (elements.Length != employees.Count)
                        throw new Exception($"Invalid response: Element count mismatch for row {i}.");
                    for (int j = 0; j < employees.Count; j++)
                    {
                        if (elements[j].Status != "OK")
                            throw new Exception($"Distance calculation failed between employees {employees[i].ColaboradorId} and {employees[j].ColaboradorId}.");
                        matrix[i, j] = elements[j].Distance.Value / 1000.0; // Convertir a km
                    }
                }
                return matrix;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to connect to Google Maps API: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to parse Google API response: {ex.Message}", ex);
            }
        }

        private List<List<int>> HierarchicalClustering(double[,] distances, double maxDistance)
        {
            int n = distances.GetLength(0);
            var clusters = Enumerable.Range(0, n).Select(i => new List<int> { i }).ToList();
            var minDistances = new List<(int cluster1, int cluster2, double distance)>();

            // Inicializar las distancias mínimas entre clusters
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    minDistances.Add((i, j, distances[i, j]));
                }
            }

            while (minDistances.Any())
            {
                // Encontrar la distancia mínima entre clusters
                var closest = minDistances.OrderBy(d => d.distance).First();
                if (closest.distance > maxDistance)
                    break; // Detener si la distancia mínima excede el umbral

                var cluster1 = clusters[closest.cluster1];
                var cluster2 = clusters[closest.cluster2];

                // Combinar los clusters
                cluster1.AddRange(cluster2);
                clusters.Remove(cluster2);

                // Actualizar las distancias mínimas
                minDistances.RemoveAll(d => d.cluster1 == closest.cluster1 || d.cluster2 == closest.cluster1 ||
                                           d.cluster1 == closest.cluster2 || d.cluster2 == closest.cluster2);
                for (int i = 0; i < clusters.Count; i++)
                {
                    if (i != closest.cluster1)
                    {
                        double minDist = double.MaxValue;
                        foreach (var idx1 in clusters[closest.cluster1])
                        {
                            foreach (var idx2 in clusters[i])
                            {
                                minDist = Math.Min(minDist, distances[idx1, idx2]);
                            }
                        }
                        minDistances.Add((closest.cluster1, i, minDist));
                    }
                }
            }

            return clusters;
        }

    }
}
