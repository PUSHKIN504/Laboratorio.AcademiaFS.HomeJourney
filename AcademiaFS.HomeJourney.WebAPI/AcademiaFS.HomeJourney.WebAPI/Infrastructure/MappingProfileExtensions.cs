using AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
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
            CreateMap<Usuarios, UsuarioConDetallesDto>().ReverseMap();
            CreateMap<Cargos, CargoDto>().ReverseMap();
            CreateMap<Ciudades, CiudadesDto>().ReverseMap();
            CreateMap<Departamentos, DepartamentoDto>().ReverseMap();
            CreateMap<Estados, EstadoDto>().ReverseMap();
            CreateMap<Estadosciviles, EstadoCivilDto>().ReverseMap();
            CreateMap<Serviciostransportes, ServicioTransporteDto>().ReverseMap()
                .ForMember(dest => dest.Usuariomodifica, opt => opt.Ignore());

            CreateMap<Usuarios, UsuarioConDetallesDto>()
            .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.PersonaNombreCompleto, opt => opt.MapFrom(src => $"{src.Colaborador.Persona.Nombre} {src.Colaborador.Persona.Apelllido}"))
            .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Colaborador.Cargo != null ? src.Colaborador.Cargo.Nombre : "Sin cargo"))
            .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Colaborador.Rol != null ? src.Colaborador.Rol.Nombre : "Sin rol"));

        }
    }
}
