using AcademiaFS.HomeJourney.WebAPI._Common;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class DomainServiceViaje
    {
        private readonly HomeJourneyContext _context;
        private readonly IGoogleMapsService _googleMapsService;

        public DomainServiceViaje(HomeJourneyContext context, IGoogleMapsService googleMapsService)
        {
            _context = context;
            _googleMapsService = googleMapsService;
        }
        public async Task ValidateAndSetDistanceAsync(Colaboradoressucursales entity)
        {
            // Validar que el colaborador existe y está activo
            var colaborador = _context.Colaboradores
                .FirstOrDefault(c => c.ColaboradorId == entity.ColaboradorId && c.Activo)
                ?? throw new ArgumentException($"El colaborador con ID {entity.ColaboradorId} no existe o no está activo.");

            // Validar que la sucursal existe y está activa
            var sucursal = _context.Sucursales
                .FirstOrDefault(s => s.SucursalId == entity.SucursalId && s.Activo)
                ?? throw new ArgumentException($"La sucursal con ID {entity.SucursalId} no existe o no está activa.");

            // Validar que no exista un registro previo con el mismo ColaboradorId y SucursalId
            var existingAssignment = _context.Colaboradoressucursales
                .FirstOrDefault(cs => cs.ColaboradorId == entity.ColaboradorId
                                   && cs.SucursalId == entity.SucursalId
                                   && cs.Activo);
            if (existingAssignment != null)
                throw new ArgumentException($"El colaborador {entity.ColaboradorId} ya está asignado a la sucursal {entity.SucursalId}.");

            // Obtener coordenadas y calcular distancia con Google Maps
            var locations = new List<ViajesdetallesCreateClusteredDto>
            {
                new ViajesdetallesCreateClusteredDto
                {
                    Latitud = (double)sucursal.Latitud,
                    Longitud = (double)sucursal.Longitud
                },
                new ViajesdetallesCreateClusteredDto
                {
                    Latitud = (double)colaborador.Latitud,
                    Longitud = (double)colaborador.Longitud
                }
            };

            var distanceMatrix = await _googleMapsService.GetDistanceMatrixAsync(locations);
            var distanceKm = (decimal)distanceMatrix[0, 1]; // Distancia de sucursal (0) a colaborador (1)

            // Validar la distancia
            if (distanceKm <= 0)
                throw new ArgumentException("La distancia calculada no puede ser cero o negativa.");
            if (distanceKm > 50)
                throw new ArgumentException($"La distancia calculada ({distanceKm} km) excede el límite de 50 km.");

            // Asignar la distancia calculada a la entidad
            entity.Distanciakilometro = distanceKm;
        }
    }
}
