using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals
{
    public class PersonasColaboradoresService
    {
        private readonly HomeJourneyContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonasColaboradoresService(HomeJourneyContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Personas> CreatePersonaColaboradorAsync(CreatePersonaColaboradorDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var persona = _mapper.Map<Personas>(dto);
                persona.Fechacrea = DateTime.Now;

                _context.Personas.Add(persona);
                await _unitOfWork.SaveAsync();

                var colaborador = _mapper.Map<Colaboradores>(dto);
                colaborador.PersonaId = persona.PersonaId;
                colaborador.Fechacrea = DateTime.Now;
                _context.Colaboradores.Add(colaborador);
                await _unitOfWork.SaveAsync();

                await _unitOfWork.CommitTransactionAsync();

                return persona;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<Personas> GetByIdAsync(int id)
        {
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.PersonaId == id);
            return persona;
        }
    }
}
