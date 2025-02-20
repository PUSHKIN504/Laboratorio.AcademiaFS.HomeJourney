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

        public UsuarioService(HomeJourneyContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
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
                .FirstOrDefault(u => u.Username == username && u.Activo);

            if (usuario == null)
                throw new Exception("Usuario no encontrado o inactivo.");

            if (!ValidatePassword(password, usuario.Passwordhash))
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



        private bool ValidatePassword(string inputPassword, byte[] storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(inputPassword);
                var inputHash = sha256.ComputeHash(inputBytes);
                return inputHash.SequenceEqual(storedHash);
            }
        }
    }
}
