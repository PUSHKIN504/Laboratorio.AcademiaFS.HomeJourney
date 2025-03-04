using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcademiaFS.HomeJourney.WebAPI._Common;

public class ClusteringApplicationService
{
    private readonly IGoogleMapsService _googleMapsService;
    private readonly HomeJourneyContext _context;
    private readonly DomainServiceClustering _domainServiceClustering;

    public ClusteringApplicationService(
        IGoogleMapsService googleMapsService,
        HomeJourneyContext context,
        DomainServiceClustering domainServiceClustering)
    {
        _googleMapsService = googleMapsService;
        _context = context;
        _domainServiceClustering = domainServiceClustering;
    }

    public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
        List<ViajesdetallesCreateClusteredDto> employees,
        decimal distanceThreshold)
    {
        if (distanceThreshold <= 0)
            throw new ArgumentException("El umbral de distancia debe ser positivo.");
        if (employees == null || !employees.Any())
            throw new ArgumentException("La lista de empleados no puede ser nula o vacía.");

        // Llamada al servicio externo para obtener la matriz de distancias.
        var distanceMatrix = await _googleMapsService.GetDistanceMatrixAsync(employees);
        var clusters = _domainServiceClustering.PerformHierarchicalClustering(distanceMatrix, (double)distanceThreshold);
        var adjustedClusters = _domainServiceClustering.AdjustClustersForDistanceLimit(clusters, employees, 100m);

        return _domainServiceClustering.MapClustersToEmployees(employees, adjustedClusters);
    }

    public List<Viajes> CreateTripsFromClusters(
        ViajesCreateClusteredDto tripDto,
        List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
    {
        if (tripDto == null || tripDto.TransportistaIds == null)
            throw new ArgumentNullException(nameof(tripDto), "Los datos del viaje no pueden ser nulos.");
        if (tripDto.TransportistaIds.Count < clusteredEmployees.Count)
            throw new ArgumentException("No hay suficientes transportistas para los clústeres.");

        var trips = new List<Viajes>();
        var availableTransportistas = new List<int>(tripDto.TransportistaIds);

        foreach (var cluster in clusteredEmployees)
        {
            int transportistaId = _domainServiceClustering.AssignTransportista(availableTransportistas);
            var transportista = GetTransportista(transportistaId);
            var tripDetails = _domainServiceClustering.BuildTripDetails(cluster, tripDto, transportista.Tarifaporkilometro);
            trips.Add(_domainServiceClustering.BuildTrip(tripDto, tripDetails, transportistaId));
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
}
