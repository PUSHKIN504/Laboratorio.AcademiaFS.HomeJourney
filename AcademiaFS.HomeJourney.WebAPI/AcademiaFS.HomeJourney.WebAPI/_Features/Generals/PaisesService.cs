using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals
{
    public class PaisesService
    {
        private readonly HomeJourneyContext _homeJourneyContext;
        private readonly IMapper _mapper;

        public PaisesService(HomeJourneyContext homeJourneyContext, IMapper mapper)
        {
            _homeJourneyContext = homeJourneyContext;
            _mapper = mapper;
        }

        public List<PaisesDto> ListadoPaises()
        {
            var listado = _homeJourneyContext.Paises.AsNoTracking().ToList();
            return _mapper.Map<List<PaisesDto>>(listado);
        }

        // Crear un nuevo país
        public PaisesDto CrearPais(PaisesDto paisDto)
        {
            var entidad = _mapper.Map<Paises>(paisDto);
            //entidad.Activo = true;
            _homeJourneyContext.Paises.Add(entidad);
            _homeJourneyContext.SaveChanges();
            return _mapper.Map<PaisesDto>(entidad);
        }

        public PaisesDto EditarPais(PaisesDto paisDto)
        {
            var entidadExistente = _homeJourneyContext.Paises.Find(paisDto.PaisId);
            if (entidadExistente == null)
            {
                throw new Exception("El país no existe");
            }
            entidadExistente.Nombre = paisDto.Nombre;
            _homeJourneyContext.SaveChanges();
            return _mapper.Map<PaisesDto>(entidadExistente);
        }

        // Desactivar (soft delete) un país
        public void DesactivarPais(int paisId, bool estado)
        {
            var entidad = _homeJourneyContext.Paises.Find(paisId);
            if (entidad == null)
            {
                throw new Exception("El país no existe");
            }
            //entidad.Activo = estado;
            _homeJourneyContext.SaveChanges();
        }
    }
}
