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
            CreateMap<CreatePersonaColaboradorDto, Personas>()
            .ForMember(dest => dest.PersonaId, opt => opt.Ignore());
            CreateMap<CreatePersonaColaboradorDto, Colaboradores>()
                .ForMember(dest => dest.ColaboradorId, opt => opt.Ignore()) 
                .ForMember(dest => dest.PersonaId, opt => opt.Ignore());
            CreateMap<Personas, PersonaDto>();
            CreateMap<CreateTransportistaDto, Personas>()
                .ForMember(dest => dest.PersonaId, opt => opt.Ignore())
                .ForMember(dest => dest.Fechacrea, opt => opt.Ignore());

            CreateMap<CreateTransportistaDto, Transportistas>()
                .ForMember(dest => dest.PersonaId, opt => opt.Ignore())
                .ForMember(dest => dest.Fechacrea, opt => opt.Ignore());
            CreateMap<ViajesCreateDto, Viajes>().ReverseMap();
            CreateMap<ViajesCreateDto, Viajesdetalles>().ReverseMap();
            CreateMap<ViajesCreateDto, Viajesdetalles>().ReverseMap();
            CreateMap<ColaboradorSucursalRequestDto, Colaboradoressucursales>().ReverseMap();
            //CreateMap<ColaboradorSucursalDto, Colaboradoressucursales>().ReverseMap();
            CreateMap<Colaboradoressucursales, ColaboradorSucursalDto>()
            .ForMember(dest => dest.NombreColaborador, opt => opt.MapFrom(src =>
                 src.Colaborador != null && src.Colaborador.Persona != null
                    ? src.Colaborador.Persona.Nombre + " " + src.Colaborador.Persona.Apelllido
                    : "Nombre no disponible"))
            .ForMember(dest => dest.NombreSucursal, opt => opt.MapFrom(src =>
                 src.Sucursal != null ? src.Sucursal.Nombre : "Sucursal no disponible"));

            CreateMap<SolicitudViajeCreateDto, Solicitudesviajes>().ReverseMap();
            CreateMap<Usuarios, UsuarioConDetallesDto>()
                        .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
                        .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                        .ForMember(dest => dest.PersonaNombreCompleto, opt => opt.MapFrom(src =>
                            src.Colaborador != null && src.Colaborador.Persona != null
                            ? $"{src.Colaborador.Persona.Nombre} {src.Colaborador.Persona.Apelllido}"
                            : "Sin nombre"))
                        .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src =>
                            src.Colaborador != null && src.Colaborador.Cargo != null
                            ? src.Colaborador.Cargo.Nombre
                            : "Sin cargo"))
                        .ForMember(dest => dest.Rol, opt => opt.MapFrom(src =>
                            src.Colaborador != null && src.Colaborador.Rol != null
                            ? src.Colaborador.Rol.Nombre
                            : "Sin rol"))
                        .AfterMap((src, dest) =>
                        {
                            if (src.Colaborador != null && src.Colaborador.CargoId == 3 && src.Colaborador.Sucursales.Any(s => s.Activo))
                            {
                                var sucursal = src.Colaborador.Sucursales.First(s => s.Activo);
                                dest.SucursalId = sucursal.SucursalId;
                                dest.SucursalNombre = sucursal.Nombre;
                            }
                            else
                            {
                                dest.SucursalId = null;
                                dest.SucursalNombre = null;
                            }
                        });
            CreateMap<Colaboradores, ColaboradorGetAllDto>()
                        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Persona.Nombre))
                        .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Persona.Apelllido));

            CreateMap<Transportistas, TransportistaGetAllDto>()
                        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Persona.Nombre))
                        .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Persona.Apelllido));

        }
    }
}
