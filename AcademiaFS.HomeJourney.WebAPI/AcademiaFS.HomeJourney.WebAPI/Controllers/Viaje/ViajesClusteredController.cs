using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/viajesclustered")]
public class ViajesClusteredController : ControllerBase
{
    private readonly ViajesService _tripService;
    private const int GERENTE_TIENDA_CARGO_ID = 3;

    public ViajesClusteredController(ViajesService tripService)
    {
        _tripService = tripService;
    }

    [HttpPost("cluster-employees")]
    public async Task<IActionResult> ClusterEmployees(
        [FromQuery] int usuarioCrea,
        [FromBody] List<ViajesdetallesCreateClusteredDto> employees,
        [FromQuery] decimal distanceThreshold)
    {
        try
        {
            if (!await IsUserGerenteTienda(usuarioCrea))
                return BadRequest("Solo los gerentes de tienda pueden registrar viajes.");

            var clustered = await _tripService.ClusterEmployeesAsync(employees, distanceThreshold);
            return Ok(clustered);
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (Exception ex) { return StatusCode(500, $"Error: {ex.Message}"); }
    }
    [HttpGet("reporte-viajes-transportista")]
    public async Task<IActionResult> GetReporteViajesPorTransportista(
        [FromQuery] int usuarioCrea,
        [FromQuery] DateTime fechaInicio,
        [FromQuery] DateTime fechaFin,
        [FromQuery] int? transportistaId = null)
    {
        try
        {
            if (!await IsUserGerenteTienda(usuarioCrea))
                return BadRequest("Solo los gerentes de tienda pueden registrar viajes.");

            var reportes = await _tripService.GetViajesPorTransportistaReporteAsync(fechaInicio, fechaFin, transportistaId);
            return Ok(reportes);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized($"Acceso no autorizado: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al generar el reporte: {ex.Message}");
        }
    }

    [HttpPost("create-trips-from-clusters")]
    public async Task<IActionResult> CreateTripsFromClusters(
        [FromQuery] int usuarioCrea, 
        [FromBody] CreateViajesRequest request)
    {
        try
        {
            if (!await IsUserGerenteTienda(usuarioCrea))
                return BadRequest("Solo los gerentes de tienda pueden registrar viajes.");

            request.viajeclusteredDto.Usuariocrea = usuarioCrea;
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

    private async Task<bool> IsUserGerenteTienda(int usuarioCrea)
    {
        return await _tripService.IsUserGerenteTienda(usuarioCrea);
    }
}

public class CreateViajesRequest
{
    public ViajesCreateClusteredDto viajeclusteredDto { get; set; }
    public List<List<ViajesdetallesCreateClusteredDto>> empleadosclusteredDto { get; set; }
}