﻿using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using System.Text.Json;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class DomainServiceClustering
    {
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

            var distanceMatrix = await GetDistanceMatrixAsync(employees);
            var clusters = HierarchicalClustering(distanceMatrix, (double)distanceThreshold);

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
                        matrix[i, j] = elements[j].Distance.Value / 1000.0;
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

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    minDistances.Add((i, j, distances[i, j]));
                }
            }

            while (minDistances.Any())
            {
                var closest = minDistances.OrderBy(d => d.distance).First();
                if (closest.distance > maxDistance)
                    break;

                if (closest.cluster1 >= clusters.Count || closest.cluster2 >= clusters.Count)
                {
                    Console.WriteLine($"Índice fuera de rango: cluster1={closest.cluster1}, cluster2={closest.cluster2}, clusters.Count={clusters.Count}");
                    break;
                }

                var cluster1 = clusters[closest.cluster1];
                var cluster2 = clusters[closest.cluster2];

                cluster1.AddRange(cluster2);
                clusters.RemoveAt(closest.cluster2);


                var newMinDistances = new List<(int cluster1, int cluster2, double distance)>();
                for (int i = 0; i < clusters.Count; i++)
                {
                    for (int j = i + 1; j < clusters.Count; j++)
                    {
                        double minDist = double.MaxValue;
                        foreach (var idx1 in clusters[i])
                        {
                            foreach (var idx2 in clusters[j])
                            {
                                if (idx1 >= distances.GetLength(0) || idx2 >= distances.GetLength(0))
                                    throw new IndexOutOfRangeException($"Índice inválido en distances: idx1={idx1}, idx2={idx2}, tamaño={distances.GetLength(0)}");
                                minDist = Math.Min(minDist, distances[idx1, idx2]);
                            }
                        }
                        newMinDistances.Add((i, j, minDist));
                    }
                }
                minDistances = newMinDistances; 
            }

            return clusters;
        }
    }

    
}