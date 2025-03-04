using AcademiaFS.HomeJourney.WebAPI._Common;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class DomainServiceViaje
    {
        public void ValidateAndSetDistance(
          Colaboradoressucursales entity,
          Colaboradores colaborador,
          Sucursales sucursal,
          decimal distanceKm)
        {
            if (!colaborador.Activo)
                throw new ArgumentException($"El colaborador con ID {colaborador.ColaboradorId} no está activo.");

            if (!sucursal.Activo)
                throw new ArgumentException($"La sucursal con ID {sucursal.SucursalId} no está activa.");

            if (distanceKm <= 0)
                throw new ArgumentException("La distancia calculada no puede ser cero o negativa.");
            if (distanceKm > 50)
                throw new ArgumentException($"La distancia calculada ({distanceKm} km) excede el límite de 50 km.");

            entity.Distanciakilometro = distanceKm;
        }



        public void ValidateFechaNoEnPasado(DateTime fecha, string nombreCampo)
        {
            if (fecha.Date < DateTime.Now.Date)
                throw new ArgumentException($"La {nombreCampo} no puede estar en el pasado.");
        }

        public void ValidateCoordenadas(decimal? latitud, decimal? longitud, string nombreEntidad)
        {
            if (latitud == null || longitud == null)
                throw new ArgumentException($"Las coordenadas de {nombreEntidad} no pueden ser nulas.");
            if (latitud < -90 || latitud > 90)
                throw new ArgumentException($"La latitud de {nombreEntidad} debe estar entre -90 y 90.");
            if (longitud < -180 || longitud > 180)
                throw new ArgumentException($"La longitud de {nombreEntidad} debe estar entre -180 y 180.");
        }

        public void ValidateDistanceThreshold(decimal distanceThreshold)
        {
            if (distanceThreshold < 0)
                throw new ArgumentException("El umbral de distancia debe ser un valor positivo o cero.");
        }

        public void ValidateTripInputs(ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
        {
            if (tripDto == null || tripDto.SucursalId <= 0 || tripDto.TransportistaIds == null || !tripDto.TransportistaIds.Any())
                throw new ArgumentException("Datos del viaje inválidos o incompletos.");
            if (clusteredEmployees == null || !clusteredEmployees.Any())
                throw new ArgumentException("Los empleados clusterizados no pueden estar vacíos.");

            ValidateFechaNoEnPasado(tripDto.Viajefecha, "fecha del viaje");

            if (tripDto.TransportistaIds.Distinct().Count() != tripDto.TransportistaIds.Count)
                throw new ArgumentException("La lista de transportistas contiene elementos duplicados.");
        }
    }
}
