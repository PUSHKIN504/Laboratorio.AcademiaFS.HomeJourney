using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

public class DomainServiceClustering
{
    private readonly Random _random = new Random();

    public List<List<int>> AdjustClustersForDistanceLimit(
        List<List<int>> clusters,
        List<ViajesdetallesCreateClusteredDto> employees,
        decimal maxDistance)
    {
        var adjustedClusters = new List<List<int>>();

        foreach (var cluster in clusters)
        {
            var currentCluster = new List<int>();
            decimal totalDistance = 0m;

            foreach (var employeeIndex in cluster.OrderBy(i => employees[i].Distanciakilometros))
            {
                decimal employeeDistance = employees[employeeIndex].Distanciakilometros;

                if (totalDistance + employeeDistance <= maxDistance)
                {
                    currentCluster.Add(employeeIndex);
                    totalDistance += employeeDistance;
                }
                else
                {
                    if (currentCluster.Any())
                    {
                        adjustedClusters.Add(new List<int>(currentCluster));
                    }
                    currentCluster = new List<int> { employeeIndex };
                    totalDistance = employeeDistance;
                }
            }
            if (currentCluster.Any())
            {
                adjustedClusters.Add(currentCluster);
            }
        }
        return adjustedClusters;
    }

    public List<List<int>> PerformHierarchicalClustering(double[,] distances, double maxDistance)
    {
        int n = distances.GetLength(0);
        var clusters = Enumerable.Range(0, n).Select(i => new List<int> { i }).ToList();
        var minDistances = CalculateInitialDistances(distances, n);

        while (minDistances.Any())
        {
            var closest = minDistances.OrderBy(d => d.distance).First();
            if (closest.distance > maxDistance)
                break;

            MergeClusters(clusters, closest.cluster1, closest.cluster2);
            minDistances = RecalculateDistances(distances, clusters);
        }
        return clusters;
    }

    public List<(int cluster1, int cluster2, double distance)> CalculateInitialDistances(double[,] distances, int n)
    {
        var minDistances = new List<(int, int, double)>();
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                minDistances.Add((i, j, distances[i, j]));
            }
        }
        return minDistances;
    }

    public void MergeClusters(List<List<int>> clusters, int cluster1Idx, int cluster2Idx)
    {
        var cluster1 = clusters[cluster1Idx];
        var cluster2 = clusters[cluster2Idx];
        cluster1.AddRange(cluster2);
        clusters.RemoveAt(cluster2Idx);
    }

    public List<(int cluster1, int cluster2, double distance)> RecalculateDistances(
        double[,] distances,
        List<List<int>> clusters)
    {
        var newMinDistances = new List<(int, int, double)>();
        for (int i = 0; i < clusters.Count; i++)
        {
            for (int j = i + 1; j < clusters.Count; j++)
            {
                double minDist = clusters[i]
                    .SelectMany(idx1 => clusters[j].Select(idx2 => distances[idx1, idx2]))
                    .Min();
                newMinDistances.Add((i, j, minDist));
            }
        }
        return newMinDistances;
    }

    public List<List<ViajesdetallesCreateClusteredDto>> MapClustersToEmployees(
        List<ViajesdetallesCreateClusteredDto> employees,
        List<List<int>> clusters)
    {
        var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>();
        foreach (var cluster in clusters)
        {
            var group = employees.Where((_, idx) => cluster.Contains(idx)).ToList();
            if (group.Any())
                clusteredEmployees.Add(group);
        }
        return clusteredEmployees;
    }

    public List<Viajesdetalles> BuildTripDetails(
        List<ViajesdetallesCreateClusteredDto> cluster,
        ViajesCreateClusteredDto tripDto,
        decimal tarifaPorKilometro)
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

    public int AssignTransportista(List<int> availableTransportistas)
    {
        int randomIndex = _random.Next(0, availableTransportistas.Count);
        int transportistaId = availableTransportistas[randomIndex];
        availableTransportistas.RemoveAt(randomIndex);
        return transportistaId;
    }

    public Viajes BuildTrip(
        ViajesCreateClusteredDto tripDto,
        List<Viajesdetalles> tripDetails,
        int transportistaId)
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
}
