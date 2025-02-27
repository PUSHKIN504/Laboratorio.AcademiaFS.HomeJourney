using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class ViajesService
{
    private readonly HomeJourneyContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DomainServiceClustering _clusteringService;
    private const int GERENTE_TIENDA_CARGO_ID = 3;
    public ViajesService(HomeJourneyContext context, IUnitOfWork unitOfWork, DomainServiceClustering clusteringService)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _clusteringService = clusteringService;
    }

    public async Task<List<ViajesTransportistaReporteDto>> GetViajesPorTransportistaReporteAsync(
                DateTime fechaInicio,
                DateTime fechaFin,
                int? transportistaId = null)
    {
        var query = _context.Viajes
            .Where(v => v.Viajefecha >= fechaInicio && v.Viajefecha <= fechaFin && v.Activo && v.EstadoId==4);

        if (transportistaId.HasValue)
        {
            query = query.Where(v => v.TransportistaId == transportistaId.Value);
        }

        var viajes = await query
            .Include(v => v.Viajesdetalles)
            .Include(v => v.Transportista) // Incluye Transportista
            .ThenInclude(t => t.Persona) // Incluye Persona relacionada con Transportista
            .ToListAsync();

        var reportes = viajes
            .GroupBy(v => v.TransportistaId)
            .Select(g => new ViajesTransportistaReporteDto
            {
                TransportistaId = g.Key,
                TransportistaNombre = g.First().Transportista?.Persona?.Nombre ?? "Sin nombre",
                DNI = g.First().Transportista?.Persona?.Documentonacionalidentificacion ?? "Sin DNI",
                Correo = g.First().Transportista?.Persona?.Email ?? "Sin correo",
                Viajes = g.Select(v => new ViajeDetalleReporteDto
                {
                    ViajeId = v.ViajeId,
                    Viajefecha = v.Viajefecha,
                    Viajehora = v.Viajehora,
                    Totalkilometros = v.Totalkilometros,
                    Totalpagar = v.Totalpagar,
                    Detalles = v.Viajesdetalles.Select(d => new ViajesdetallesReporteDto
                    {
                        ColaboradorId = d.ColaboradorId,
                        Distanciakilometros = d.Distanciakilometros,
                        Totalpagar = d.Totalpagar,
                        ColaboradorsucursalId = d.ColaboradorsucursalId
                    }).ToList()
                }).ToList(),
                TotalPagar = g.Sum(v => v.Totalpagar)
            }).ToList();

        return reportes;
    }
    public Colaboradores GetColaboradorById(int id) => _context.Colaboradores.Find(id);
    public async Task<bool> IsUserGerenteTienda(int usuarioId)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Colaborador)
            .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId && u.Activo);

        return usuario != null && usuario.Colaborador != null && usuario.Colaborador.CargoId == GERENTE_TIENDA_CARGO_ID;
    }

    public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
        List<ViajesdetallesCreateClusteredDto> employees, decimal distanceThreshold)
    {
        ValidateEmployeeInputs(employees);
        return await _clusteringService.ClusterEmployeesAsync(employees, distanceThreshold);
    }

    public async Task<List<Viajes>> CreateTripsFromClustersAsync(
        ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
    {
        ValidateTripInputs(tripDto, clusteredEmployees);
        await ValidateEmployeeTripConstraints(clusteredEmployees, tripDto.Viajefecha);

        var trips = _clusteringService.CreateTripsFromClusters(tripDto, clusteredEmployees);
        ValidateTotalDistance(trips);

        await SaveTripsAsync(trips);
        return trips;
    }
    public async Task<List<Colaboradores>> GetEmployeesByBranchAsync(int sucursalId)
    {
        return await _context.Colaboradoressucursales
            .Where(cs => cs.SucursalId == sucursalId && cs.Activo)
            .Select(cs => cs.Colaborador)
            .ToListAsync();
    }

    private void ValidateEmployeeInputs(List<ViajesdetallesCreateClusteredDto> employees)
    {
        if (employees == null || !employees.Any())
            throw new ArgumentException("La lista de empleados no puede ser nula o vacía.");

        foreach (var emp in employees)
        {
            var colaboradorSucursal = _context.Colaboradoressucursales
                .FirstOrDefault(cs => cs.ColaboradorsucursalId == emp.ColaboradorsucursalId);
            if (colaboradorSucursal == null)
                throw new ArgumentException($"ColaboradorSucursalId {emp.ColaboradorsucursalId} no existe.");

            if (emp.Distanciakilometros != colaboradorSucursal.Distanciakilometro)
                throw new ArgumentException("La distancia no puede ser modificada.");
        }
    }

    private void ValidateTripInputs(ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
    {
        if (tripDto == null || tripDto.SucursalId <= 0 || tripDto.TransportistaIds == null || !tripDto.TransportistaIds.Any())
            throw new ArgumentException("Datos del viaje inválidos o incompletos.");
        if (clusteredEmployees == null || !clusteredEmployees.Any())
            throw new ArgumentException("Los empleados clusterizados no pueden estar vacíos.");
    }

    private async Task ValidateEmployeeTripConstraints(List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees, DateTime viajeFecha)
    {
        var allEmployeeIds = clusteredEmployees.SelectMany(c => c).Select(e => e.ColaboradorId).Distinct();
        var existingTrips = await _context.Viajesdetalles
            .Where(vd => allEmployeeIds.Contains(vd.ColaboradorId) && vd.Viaje.Viajefecha == viajeFecha.Date)
            .ToListAsync();

        if (existingTrips.Any())
            throw new ArgumentException("Uno o más colaboradores ya tienen un viaje registrado para este día.");
    }

    private void ValidateTotalDistance(List<Viajes> trips)
    {
        foreach (var trip in trips)
        {
            if (trip.Totalkilometros > 100)
                throw new ArgumentException($"El viaje {trip.ViajeId} excede el límite de 100 km.");
        }
    }

    public async Task<Viajes> CreateViajeWithDetailsAsync(Viajes viaje, List<Viajesdetalles> detalles)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                _context.Viajes.Add(viaje);
                await _unitOfWork.SaveAsync();

                foreach (var detalle in detalles)
                {
                    detalle.ViajeId = viaje.ViajeId;
                    _context.Viajesdetalles.Add(detalle);
                }
                await _unitOfWork.SaveAsync();

                await transaction.CommitAsync();
                return viaje;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    private async Task SaveTripsAsync(List<Viajes> trips)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _context.Viajes.AddRange(trips);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                throw new Exception("No se guardaron cambios en la base de datos.");
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new Exception($"Error al guardar los viajes: {ex.Message}", ex);
        }
    }
}