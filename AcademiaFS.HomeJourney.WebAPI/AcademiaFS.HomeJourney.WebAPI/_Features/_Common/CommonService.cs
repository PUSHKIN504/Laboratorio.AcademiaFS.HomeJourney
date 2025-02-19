using AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
//using Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common.Entities;
//using Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common
{
    public class CommonService
    {
        //private readonly LogisticContext _logisticContext;
        private readonly HomeJourneyContext _homeJourneyContext;

        public CommonService(
            HomeJourneyContext homeJourneyContext
            //LogisticContext logisticContext
            )
        {
            _homeJourneyContext = homeJourneyContext;
            //_logisticContext = logisticContext;
        }

        //public List<Usuario> ListadoUsuarios()
        //{
        //    var listado = _logisticContext.Usuarios.AsNoTracking().ToList();

        //    return listado;
        //}

        public List<Paises> ListadoCiudades()
        {
            var listado = _homeJourneyContext.Paises.AsNoTracking().ToList();

            return listado;
        }
    }
}
