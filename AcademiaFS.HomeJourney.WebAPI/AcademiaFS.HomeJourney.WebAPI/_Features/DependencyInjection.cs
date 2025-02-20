using AcademiaFS.HomeJourney.WebAPI._Features.Auth;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals;
using Microsoft.Extensions.DependencyInjection;

namespace AcademiaFS.HomeJourney.WebAPI._Features
{
    public static class DependencyInjection
    {
        public static IServiceCollection AppAplication (IServiceCollection service)
        {
            service.AddTransient<UsuarioService>();
            service.AddTransient(typeof(IGenericServiceInterface<,>), typeof(GenericService<,>));
            return service;
        }
    }

}
