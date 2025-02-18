using Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common.Entities;
using Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common
{
    public class CommonService
    {
        private readonly LogisticContext _logisticContext;
        
        public CommonService(LogisticContext logisticContext)
        {
            _logisticContext = logisticContext;
        }

        public List<Usuario> ListadoUsuarios()
        {
            var listado = _logisticContext.Usuarios.AsNoTracking().ToList();

            return listado;
        }
    }
}
