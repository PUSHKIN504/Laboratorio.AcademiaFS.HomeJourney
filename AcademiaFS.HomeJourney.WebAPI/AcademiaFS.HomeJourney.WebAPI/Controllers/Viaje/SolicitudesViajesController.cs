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
    }
}
