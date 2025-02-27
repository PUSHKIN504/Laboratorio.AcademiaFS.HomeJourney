using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using Microsoft.AspNetCore.Mvc;
using static AcademiaFS.HomeJourney.WebAPI._Features.Viaje.ViajeRequest;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Viaje
{
    [ApiController]
    [Route("api/viajesclustered")]
    public class ViajesClusteredController : Controller
    {
        private readonly ViajesService _tripService;

        public ViajesClusteredController(ViajesService tripService)
        {
            _tripService = tripService;
        }

        [HttpPost("cluster-employees")]
        public async Task<IActionResult> ClusterEmployees(
            [FromBody] List<ViajesdetallesCreateClusteredDto> employees,
            [FromQuery] decimal distanceThreshold,
            [FromQuery] int usuarioId) // podrías obtenerlo de Claims en lugar de query
        {
            try
            {
                // Pasamos el ID del usuario actual para validar si tiene cargo=3
                var clustered = await _tripService.ClusterEmployeesAsync(employees, distanceThreshold, usuarioId);
                return Ok(clustered);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        //[HttpPost("create-trips-from-clusters")]
        //public async Task<IActionResult> CreateTripsFromClusters([FromBody] CreateViajesRequest request)
        //{
        //    try
        //    {
        //        var trips = await _tripService.CreateTripsFromClustersAsync(
        //            request.viajeclusteredDto,
        //            request.empleadosclusteredDto
        //        );

        //        return Ok(trips.Select(t => new
        //        {
        //            t.ViajeId,
        //            t.SucursalId,
        //            t.TransportistaId,
        //            t.EstadoId,
        //            t.Viajehora,
        //            t.Viajefecha,
        //            t.Totalkilometros,
        //            t.Totalpagar,
        //            DetailsCount = t.Viajesdetalles.Count
        //        }));
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error: {ex.Message}");
        //    }
        //}
        [HttpPost("create-trips-from-clusters")]
        public async Task<IActionResult> CreateTripsFromClusters(
    [FromBody] CreateViajesRequest request,
    [FromQuery] decimal distanceThreshold)
        {
            try
            {
                var trips = await _tripService.CreateTripsFromClustersAsync(
                    request.viajeclusteredDto,
                    request.empleadosclusteredDto,
                    distanceThreshold // <= Se lo pasas a tu servicio
                );

                return Ok(trips.Select(t => new
                {
                    t.ViajeId,
                    t.SucursalId,
                    t.TransportistaId,
                    t.EstadoId,
                    t.Viajehora,
                    t.Viajefecha,
                    t.Totalkilometros,
                    t.Totalpagar,
                    DetailsCount = t.Viajesdetalles.Count
                }));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
