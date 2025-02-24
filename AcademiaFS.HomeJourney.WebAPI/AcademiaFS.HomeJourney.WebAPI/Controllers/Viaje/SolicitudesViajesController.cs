using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Viaje
{
    [ApiController]
    [Route("academiafarsiman/solicitudesviajes")]
    public class SolicitudesViajesController : ControllerBase
    {
        private readonly IGenericServiceInterface<Solicitudesviajes, int> _solicitudesService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SolicitudesViajesController(
            IGenericServiceInterface<Solicitudesviajes, int> solicitudesService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _solicitudesService = solicitudesService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPatch("{id}/aprobar")]
        public IActionResult AprobarSolicitud(int id, [FromBody] SolicitudViajeAprobacionDto dto)
        {
            if (id != dto.SolicitudviajeId)
            {
                return BadRequest(new CustomResponse<string>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID de la solicitud."
                });
            }

            var solicitud = _solicitudesService.GetById(id);
            if (solicitud == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró la solicitud con ID {id}."
                });
            }

            solicitud.SupervisorId = dto.SupervisorId;
            solicitud.FechaAprobacion = DateTime.Now;
            solicitud.Comentarios = dto.Comentarios; 

            solicitud.EstadoId = dto.Aprobar ? 2 : 3;

            _solicitudesService.Update(solicitud);
            _unitOfWork.Save();

            return Ok(new CustomResponse<Solicitudesviajes>
            {
                Success = true,
                Message = dto.Aprobar ? "Solicitud aprobada correctamente." : "Solicitud rechazada.",
                Data = solicitud
            });
        }

        [HttpPost("colaborador")]
        public ActionResult<CustomResponse<Solicitudesviajes>> CrearSolicitudColaborador([FromBody] SolicitudViajeCreateDto dto)
        {
            var today = DateTime.Today;

            var solicitudesDelDia = _solicitudesService
                .GetAll()
                .Where(s => s.ColaboradorId == dto.ColaboradorId && s.Fechacrea.Date == today)
                .ToList();

            if (solicitudesDelDia.Any())
            {
                return BadRequest(new CustomResponse<string>
                {
                    Success = false,
                    Message = "Ya se ha generado una solicitud para el día de hoy."
                });
            }

            var nuevaSolicitud = new Solicitudesviajes
            {
                ColaboradorId = dto.ColaboradorId,
                ViajeId = dto.ViajeId,
                Comentarios = dto.Comentarios,
                EstadoId = 1, 
                Activo = true,
                Usuariocrea = dto.ColaboradorId, 
                Fechacrea = DateTime.Now,
                Fechasolicitud = DateTime.Now
            };

            var creado = _solicitudesService.Create(nuevaSolicitud);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetById), new { id = creado.SolicitudviajeId }, new CustomResponse<Solicitudesviajes>
            {
                Success = true,
                Message = "Solicitud de viaje creada correctamente.",
                Data = creado
            });
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<Solicitudesviajes>> GetById(int id)
        {
            var solicitud = _solicitudesService.GetById(id);
            if (solicitud == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró la solicitud con ID {id}."
                });
            }

            return Ok(new CustomResponse<Solicitudesviajes>
            {
                Success = true,
                Message = "Solicitud encontrada.",
                Data = solicitud
            });
        }

        [HttpPatch("colaborador/{id}/cancelar")]
        public ActionResult<CustomResponse<Solicitudesviajes>> CancelarSolicitud(int id, [FromQuery] int colaboradorId)
        {
            var solicitud = _solicitudesService.GetById(id);
            if (solicitud == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró la solicitud con ID {id}."
                });
            }

            if (solicitud.ColaboradorId != colaboradorId)
            {
                return BadRequest(new CustomResponse<string>
                {
                    Success = false,
                    Message = "La solicitud no pertenece a este colaborador."
                });
            }

            if (solicitud.EstadoId != 1)
            {
                return BadRequest(new CustomResponse<string>
                {
                    Success = false,
                    Message = "La solicitud ya no se puede cancelar."
                });
            }

            solicitud.EstadoId = 4;
            solicitud.Usuariomodifica = colaboradorId;
            solicitud.Fechamodifica = DateTime.Now;

            _solicitudesService.Update(solicitud);
            _unitOfWork.Save();

            return Ok(new CustomResponse<Solicitudesviajes>
            {
                Success = true,
                Message = "Solicitud cancelada correctamente.",
                Data = solicitud
            });
        }
    }
}
