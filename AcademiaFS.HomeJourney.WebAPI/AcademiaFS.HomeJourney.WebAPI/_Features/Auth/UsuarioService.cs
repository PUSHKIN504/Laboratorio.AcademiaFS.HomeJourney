using AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI.Utilities;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Auth
{
    public class UsuarioService 
    {
        private readonly IMapper _mapper;
        private readonly HomeJourneyContext _context;
        private readonly DomainServiceAuth _domainService;
        public UsuarioService(HomeJourneyContext context, IMapper mapper, DomainServiceAuth domainService)
        {
            _mapper = mapper;
            _context = context;
            _domainService = domainService;
        }

   
        public CustomResponse<UsuarioConDetallesDto> Login(string username, string password)
        {
            var usuario = _context.Usuarios
                .AsNoTracking()
            .Include(u => u.Colaborador)
                .ThenInclude(c => c.Persona)
            .Include(u => u.Colaborador)
                .ThenInclude(c => c.Cargo)
            .Include(u => u.Colaborador)
                .ThenInclude(c => c.Rol)
            .Include(u => u.Colaborador)
                .ThenInclude(c => c.Sucursales)
            .FirstOrDefault(u => u.Username == username && u.Activo);

            if (usuario == null)
                throw new Exception("Usuario no encontrado o inactivo.");

            if (!_domainService.ValidatePassword(password, usuario.Passwordhash))
                throw new Exception("Contraseña incorrecta.");

            var dto = _mapper.Map<UsuarioConDetallesDto>(usuario);
                
            var response = new CustomResponse<UsuarioConDetallesDto>
            {
                Success = true,
                Message = "Usuario autenticado correctamente",
                Data = dto
            };

            return response;
        }

    }
}
