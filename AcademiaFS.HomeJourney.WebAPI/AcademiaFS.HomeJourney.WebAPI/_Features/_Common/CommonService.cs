using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AutoMapper;

//using Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common.Entities;
//using Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common
{
    public class CommonService
    {
        //private readonly LogisticContext _logisticContext;
        private readonly HomeJourneyContext _homeJourneyContext;
        //private readonly IMapper _mapper;
        public CommonService(
            HomeJourneyContext homeJourneyContext
            //IMapper mapper
            //LogisticContext logisticContext
            )
        {
            _homeJourneyContext = homeJourneyContext;
            //_mapper = mapper; 
            //_logisticContext = logisticContext;
        }

        //public List<Usuario> ListadoUsuarios()
        //{
        //    var listado = _logisticContext.Usuarios.AsNoTracking().ToList();

        //    return listado;
        //}

        public List<Paises> ListadoPaises()
            {
            var listado = _homeJourneyContext.Paises.AsNoTracking().ToList();

            return listado;
        }
    }
}
