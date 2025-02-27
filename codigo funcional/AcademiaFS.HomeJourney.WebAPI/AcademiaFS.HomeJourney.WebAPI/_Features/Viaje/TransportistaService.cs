using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class TransportistaService
    {
        private readonly HomeJourneyContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransportistaService(HomeJourneyContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Transportistas> CreateTransportistaAsync(CreateTransportistaDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var persona = _mapper.Map<Personas>(dto);
                persona.Fechacrea = DateTime.Now;

                _context.Personas.Add(persona);
                await _unitOfWork.SaveAsync();

                var transportista = _mapper.Map<Transportistas>(dto);
                transportista.PersonaId = persona.PersonaId;
                transportista.Fechacrea = DateTime.Now;
                _context.Transportistas.Add(transportista);
                await _unitOfWork.SaveAsync();

                await _unitOfWork.CommitTransactionAsync();

                return transportista;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<Transportistas?> GetByIdAsync(int id)
        {
            return await _context.Transportistas
                .Include(t => t.Persona)
                .FirstOrDefaultAsync(t => t.TransportistaId == id);
        }
    }
}
