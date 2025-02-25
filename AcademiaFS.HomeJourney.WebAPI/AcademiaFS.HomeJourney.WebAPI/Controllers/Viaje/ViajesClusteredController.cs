using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
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

        //[HttpPost("cluster-employees")]
        //public async Task<IActionResult> ClusterEmployees(
        //    [FromBody] List<ViajesdetallesCreateClusteredDto> employees,
        //    [FromQuery] decimal umbraldistancia,
        //    [FromQuery] string origin)
        //{
        //    try
        //    {
        //        var clusteredEmployees = await _tripService.ClusterEmployeesAsync(employees, umbraldistancia, origin);
        //        return Ok(clusteredEmployees.Select((cluster, index) => new
        //        {
        //            ClusterId = index,
        //            Employees = cluster.Select(e => new { e.ColaboradorId, e.Distanciakilometros, e.Latitud, e.Longitud })
        //        }));
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        //[HttpPost("create-trips-from-clusters")]
        //public async Task<IActionResult> CreateTripsFromClusters(
        //    [FromBody] CreateViajesRequest request)
        //{
        //    try
        //    {
        //        var trips = await _tripService.CreateTripsFromClustersAsync(request.viajeclusteredDto, request.empleadosclusteredDto);
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
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        //[HttpPost("cluster-employees")]
        //public async Task<IActionResult> ClusterEmployees(
        //    [FromBody] List<ViajesdetallesCreateClusteredDto> employees,
        //    [FromQuery] decimal distanceThreshold)
        //{
        //    //try
        //    //{
        //    //    var clustered = await _tripService.ClusterEmployeesAsync(employees, distanceThreshold);
        //    //    return Ok(clustered.Select((c, i) => new
        //    //    {
        //    //        ClusterId = i,
        //    //        Employees = c.Select(e => new { e.ColaboradorId, e.Distanciakilometros, e.Latitud, e.Longitud })
        //    //    }));
        //    //}
        //    //catch (ArgumentException ex) { return BadRequest(ex.Message); }
        //    //catch (Exception ex) { return StatusCode(500, $"Error: {ex.Message}"); }
        //    try
        //    {
        //        var clustered = await _tripService.ClusterEmployeesAsync(employees, distanceThreshold);
        //        // Devolver directamente la lista de listas sin proyección adicional
        //        return Ok(clustered);
        //    }
        //    catch (ArgumentException ex) { return BadRequest(ex.Message); }
        //    catch (Exception ex) { return StatusCode(500, $"Error: {ex.Message}"); }
        //}
        [HttpPost("cluster-employees")]
        public async Task<IActionResult> ClusterEmployees(
            [FromBody] List<ViajesdetallesCreateClusteredDto> employees,
            [FromQuery] decimal distanceThreshold)
        {
            try
            {
                var clustered = await _tripService.ClusterEmployeesAsync(employees, distanceThreshold);
                return Ok(clustered); // Devolver directamente la List<List<ViajesdetallesCreateClusteredDto>>
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return StatusCode(500, $"Error: {ex.Message}"); }
        }

        //[HttpPost("create-trips-from-clusters")]
        //public async Task<IActionResult> CreateTripsFromClusters(ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
        //{
        //    try
        //    {
        //        //var trips = await _tripService.CreateTripsFromClustersAsync(request.viajeclusteredDto, request.empleadosclusteredDto);
        //        var trips = _tripService.CreateTripsFromClustersAsync(tripDto, clusteredEmployees);
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
        //    catch (ArgumentException ex) { return BadRequest(ex.Message); }
        //    catch (Exception ex) { return StatusCode(500, $"Error: {ex.Message}"); }
        //}
        //[HttpPost("create-trips-from-clusters")]
        //public async Task<IActionResult> CreateTripsFromClusters([FromBody] CreateViajesRequest request)
        //{
        //    try
        //    {
        //        var trips = await _tripService.CreateTripsFromClustersAsync(request.viajeclusteredDto, request.empleadosclusteredDto);
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
        //    catch (ArgumentException ex) { return BadRequest(ex.Message); }
        //    catch (Exception ex) { return StatusCode(500, $"Error: {ex.Message}"); }
        //}
        [HttpPost("create-trips-from-clusters")]
        public async Task<IActionResult> CreateTripsFromClusters([FromBody] CreateViajesRequest request)
        {
            try
            {
                var trips = await _tripService.CreateTripsFromClustersAsync(
                    request.viajeclusteredDto,
                    request.empleadosclusteredDto
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
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return StatusCode(500, $"Error: {ex.Message}"); }
        }
    }
}
  
//}
