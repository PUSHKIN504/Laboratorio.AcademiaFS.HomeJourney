using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class ViajesService 
    {
        private readonly HomeJourneyContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ViajesService(HomeJourneyContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
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
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
