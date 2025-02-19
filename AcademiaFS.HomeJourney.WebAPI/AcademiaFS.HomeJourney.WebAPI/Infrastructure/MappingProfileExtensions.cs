using AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AutoMapper;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure
{
    public class MappingProfileExtensions : Profile
    {
        public MappingProfileExtensions()
        {
            CreateMap<Paises, PaisesDto>().ReverseMap();
            CreateMap<Pantallas, PantallaDto>().ReverseMap();
            CreateMap<Pantallasroles, PantallasrolesDto>().ReverseMap();
            CreateMap<Roles, RolesDto>().ReverseMap();
            CreateMap<Usuarios, UsuarioDto>().ReverseMap();
            CreateMap<Cargos, CargoDto>().ReverseMap();
            CreateMap<Ciudades, CargoDto>().ReverseMap();
        }
    }
}
