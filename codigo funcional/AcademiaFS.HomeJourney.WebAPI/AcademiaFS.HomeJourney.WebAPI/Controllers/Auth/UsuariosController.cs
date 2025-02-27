using AcademiaFS.HomeJourney.WebAPI._Features.Auth;
using AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Auth
{
    [ApiController]
    [Route("academiafarsiman/Usuarios")]
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public ActionResult<UsuarioLoginRequest> Login([FromBody] UsuarioLoginRequest request)
        {
            try
            {
                var usuarioDto = _usuarioService.Login(request.Username, request.Password);
                return Ok(new { Data = usuarioDto});
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

    }


}
