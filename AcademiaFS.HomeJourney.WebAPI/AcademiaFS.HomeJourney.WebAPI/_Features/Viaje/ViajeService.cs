using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class ViajesService 
    {
        private readonly HomeJourneyContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainServiceClustering _clusteringService;

        public ViajesService(HomeJourneyContext context, IUnitOfWork unitOfWork, DomainServiceClustering clusteringService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _clusteringService = clusteringService;

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
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }


        public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
            List<ViajesdetallesCreateClusteredDto> employees, decimal distanceThreshold)
        {
            return await _clusteringService.ClusterEmployeesAsync(employees, distanceThreshold);
        }


        public async Task<List<Viajes>> CreateTripsFromClustersAsync(
            ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
        {
            var trips = _clusteringService.CreateTripsFromClusters(tripDto, clusteredEmployees);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                _unitOfWork.Context.Viajes.AddRange(trips);

                var affectedRows = await _unitOfWork.SaveAsync();
                if (affectedRows == 0)
                    throw new Exception("No se guardaron cambios en la base de datos.");

                await _unitOfWork.CommitTransactionAsync();

                return trips;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception($"Error al guardar los viajes en una transacción: {ex.Message}", ex);
            }
        }
    }
}
