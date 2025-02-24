using AcademiaFS.HomeJourney.WebAPI._Features.Auth;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AcademiaFS.HomeJourney.WebAPI._Features
{
    public static class DependencyInjection
    {
        public static IServiceCollection AppAplication (IServiceCollection service)
        {
            service.AddTransient<UsuarioService>();
            service.AddTransient<DomainServiceAuth>();
            service.AddTransient<TransportistaService>();
            service.AddTransient< PersonasColaboradoresService>();
            service.AddTransient(typeof(IGenericServiceInterface<,>), typeof(GenericService<,>));
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            return service;
        }
    }

}
