using AcademiaFS.HomeJourney.WebAPI._Common;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class DomainServiceClustering
{
    private readonly IGoogleMapsService _googleMapsService;
    private readonly Random _random = new Random();
    private readonly HomeJourneyContext _context;

    public DomainServiceClustering(IGoogleMapsService googleMapsService, HomeJourneyContext context)
    {
        _googleMapsService = googleMapsService;
        _context = context;
    }

    public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
        List<ViajesdetallesCreateClusteredDto> employees, decimal distanceThreshold)
    {
        if (distanceThreshold <= 0)
            throw new ArgumentException("El umbral de distancia debe ser positivo.");
        if (employees == null || !employees.Any())
            throw new ArgumentException("La lista de empleados no puede ser nula o vacía.");

        var distanceMatrix = await _googleMapsService.GetDistanceMatrixAsync(employees);
        var clusters = PerformHierarchicalClustering(distanceMatrix, (double)distanceThreshold);

        // Redistribuir clústeres para respetar el límite de 100 km
        var adjustedClusters = AdjustClustersForDistanceLimit(clusters, employees, 100m);

        return MapClustersToEmployees(employees, adjustedClusters);
    }

    private List<List<int>> AdjustClustersForDistanceLimit(List<List<int>> clusters, List<ViajesdetallesCreateClusteredDto> employees, decimal maxDistance)
    {
        var adjustedClusters = new List<List<int>>();

        foreach (var cluster in clusters)
        {
            var currentCluster = new List<int>();
            decimal totalDistance = 0m;

            foreach (var employeeIndex in cluster.OrderBy(i => employees[i].Distanciakilometros)) // Ordenar para optimizar
            {
                decimal employeeDistance = employees[employeeIndex].Distanciakilometros;

                if (totalDistance + employeeDistance <= maxDistance)
                {
                    currentCluster.Add(employeeIndex);
                    totalDistance += employeeDistance;
                }
                else
                {
                    // Si el clúster actual no está vacío, lo agregamos
                    if (currentCluster.Any())
                    {
                        adjustedClusters.Add(new List<int>(currentCluster));
                    }

                    // Iniciar un nuevo clúster con este empleado
                    currentCluster = new List<int> { employeeIndex };
                    totalDistance = employeeDistance;
                }
            }

            // Agregar el último clúster si tiene elementos
            if (currentCluster.Any())
            {
                adjustedClusters.Add(currentCluster);
            }
        }

        return adjustedClusters;
    }

    public List<Viajes> CreateTripsFromClusters(
        ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
    {
        if (tripDto == null || tripDto.TransportistaIds == null)
            throw new ArgumentNullException(nameof(tripDto), "Los datos del viaje no pueden ser nulos.");
        if (tripDto.TransportistaIds.Count < clusteredEmployees.Count)
            throw new ArgumentException("No hay suficientes transportistas para los clústeres.");

        var trips = new List<Viajes>();
        var availableTransportistas = new List<int>(tripDto.TransportistaIds);

        foreach (var cluster in clusteredEmployees)
        {
            var transportistaId = AssignTransportista(availableTransportistas);
            var transportista = GetTransportista(transportistaId);
            var tripDetails = BuildTripDetails(cluster, tripDto, transportista.Tarifaporkilometro);
            trips.Add(BuildTrip(tripDto, tripDetails, transportistaId));
        }

        return trips;
    }

    private Transportistas GetTransportista(int transportistaId)
    {
        var transportista = _context.Transportistas
            .FirstOrDefault(t => t.TransportistaId == transportistaId && t.Activo);

        if (transportista == null)
            throw new ArgumentException($"No se encontró un transportista activo con ID {transportistaId}.");

        return transportista;
    }

    private List<Viajesdetalles> BuildTripDetails(List<ViajesdetallesCreateClusteredDto> cluster, ViajesCreateClusteredDto tripDto, decimal tarifaPorKilometro)
    {
        return cluster.Select(d => new Viajesdetalles
        {
            ColaboradorId = d.ColaboradorId,
            Distanciakilometros = d.Distanciakilometros,
            Totalpagar = tarifaPorKilometro * d.Distanciakilometros,
            ColaboradorsucursalId = d.ColaboradorsucursalId,
            Activo = true,
            Usuariocrea = tripDto.Usuariocrea,
            Fechacrea = DateTime.Now,
            MonedaId = d.MonedaId
        }).ToList();
    }

    private int AssignTransportista(List<int> availableTransportistas)
    {
        int randomIndex = _random.Next(0, availableTransportistas.Count);
        int transportistaId = availableTransportistas[randomIndex];
        availableTransportistas.RemoveAt(randomIndex);
        return transportistaId;
    }

    private Viajes BuildTrip(ViajesCreateClusteredDto tripDto, List<Viajesdetalles> tripDetails, int transportistaId)
    {
        return new Viajes
        {
            SucursalId = tripDto.SucursalId,
            TransportistaId = transportistaId,
            EstadoId = 5,
            Viajehora = tripDto.Viajehora,
            Viajefecha = tripDto.Viajefecha,
            Totalkilometros = tripDetails.Sum(d => d.Distanciakilometros),
            Totalpagar = tripDetails.Sum(d => d.Totalpagar),
            Activo = true,
            Usuariocrea = tripDto.Usuariocrea,
            Fechacrea = DateTime.Now,
            MonedaId = tripDto.MonedaId,
            Viajesdetalles = tripDetails
        };
    }

    private List<List<int>> PerformHierarchicalClustering(double[,] distances, double maxDistance)
    {
        int n = distances.GetLength(0);
        var clusters = Enumerable.Range(0, n).Select(i => new List<int> { i }).ToList();
        var minDistances = CalculateInitialDistances(distances, n);

        while (minDistances.Any())
        {
            var closest = minDistances.OrderBy(d => d.distance).First();
            if (closest.distance > maxDistance) break;

            MergeClusters(clusters, closest.cluster1, closest.cluster2);
            minDistances = RecalculateDistances(distances, clusters);
        }
        return clusters;
    }

    private List<(int cluster1, int cluster2, double distance)> CalculateInitialDistances(double[,] distances, int n)
    {
        var minDistances = new List<(int, int, double)>();
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                minDistances.Add((i, j, distances[i, j]));
        return minDistances;
    }

    private void MergeClusters(List<List<int>> clusters, int cluster1Idx, int cluster2Idx)
    {
        var cluster1 = clusters[cluster1Idx];
        var cluster2 = clusters[cluster2Idx];
        cluster1.AddRange(cluster2);
        clusters.RemoveAt(cluster2Idx);
    }

    private List<(int cluster1, int cluster2, double distance)> RecalculateDistances(double[,] distances, List<List<int>> clusters)
    {
        var newMinDistances = new List<(int, int, double)>();
        for (int i = 0; i < clusters.Count; i++)
        {
            for (int j = i + 1; j < clusters.Count; j++)
            {
                double minDist = clusters[i].SelectMany(idx1 => clusters[j].Select(idx2 => distances[idx1, idx2])).Min();
                newMinDistances.Add((i, j, minDist));
            }
        }
        return newMinDistances;
    }

    private List<List<ViajesdetallesCreateClusteredDto>> MapClustersToEmployees(
        List<ViajesdetallesCreateClusteredDto> employees, List<List<int>> clusters)
    {
        var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>();
        foreach (var cluster in clusters)
        {
            var group = employees.Where((_, idx) => cluster.Contains(idx)).ToList();
            if (group.Any()) clusteredEmployees.Add(group);
        }
        return clusteredEmployees;
    }
}