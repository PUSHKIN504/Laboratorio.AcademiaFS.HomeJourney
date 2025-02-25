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
        //private readonly TripClusteringDomainService _clusteringService;

        //public ViajesService(DomainServiceClustering clusteringService
        //    //, TripClusteringDomainService clusteringService
        //    )
        //{
        //    //_tripRepository = tripRepository;
        //    _clusteringService = clusteringService;
        //}
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

        //public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
        //    List<ViajesdetallesCreateClusteredDto> employees, decimal umbraldistancia, string origin)
        //{
        //    return await _clusteringService.ClusterEmployeesAsync(employees, umbraldistancia, origin);
        //}

        //public async Task<List<Viajes>> CreateTripsFromClustersAsync(
        //    ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
        //{
        //    var trips = _clusteringService.CreateTripsFromClusters(tripDto, clusteredEmployees);
        //    await _unitOfWork.AddRangeAsync(trips);
        //    return trips;
        //}
        public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
            List<ViajesdetallesCreateClusteredDto> employees, decimal distanceThreshold)
        {
            return await _clusteringService.ClusterEmployeesAsync(employees, distanceThreshold);
        }

        public async Task<List<Viajes>> CreateTripsFromClustersAsync(
            ViajesCreateClusteredDto tripDto, List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
        {
            var trips = _clusteringService.CreateTripsFromClusters(tripDto, clusteredEmployees);
             _unitOfWork.Save();
            return trips;
        }
    }
}
