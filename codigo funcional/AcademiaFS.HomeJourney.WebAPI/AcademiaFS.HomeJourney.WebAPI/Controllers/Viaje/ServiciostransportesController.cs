using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Viaje
{
    [ApiController]
    [Route("academiafarsiman/serviciostransportes")]
    public class ServiciostransportesController : ControllerBase
    {
        private readonly IGenericServiceInterface<Serviciostransportes, int> _servicioService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiciostransportesController(
            IGenericServiceInterface<Serviciostransportes, int> servicioService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _servicioService = servicioService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<ServicioTransporteDto>>> GetAll()
        {
            var list = _servicioService.GetAll();
            var dtoList = _mapper.Map<List<ServicioTransporteDto>>(list);

            var response = new CustomResponse<IEnumerable<ServicioTransporteDto>>
            {
                Success = true,
                Message = "Listado de servicios de transporte obtenido correctamente",
                Data = dtoList
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<ServicioTransporteDto>> GetById(int id)
        {
            var servicio = _servicioService.GetById(id);
            if (servicio == null)
            {
                return NotFound(new CustomResponse<ServicioTransporteDto>
                {
                    Success = false,
                    Message = $"No se encontró el servicio con ID {id}"
                });
            }

            var dto = _mapper.Map<ServicioTransporteDto>(servicio);
            var response = new CustomResponse<ServicioTransporteDto>
            {
                Success = true,
                Message = "Servicio encontrado",
                Data = dto
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<CustomResponse<ServicioTransporteDto>> Create([FromBody] ServicioTransporteDto dto)
        {
            var entity = _mapper.Map<Serviciostransportes>(dto);
            entity.Activo = true;

            var creado = _servicioService.Create(entity);
            //_unitOfWork.Save();

            var dtoCreado = _mapper.Map<ServicioTransporteDto>(creado);

            var response = new CustomResponse<ServicioTransporteDto>
            {
                Success = true,
                Message = "Servicio creado correctamente",
                Data = dtoCreado
            };

            return CreatedAtAction(nameof(GetById), new { id = dtoCreado.ServiciotransporteId }, response);
        }

        [HttpPut("{id}")]
        public ActionResult<CustomResponse<ServicioTransporteDto>> Update(int id, [FromBody] Serviciostransportes servicio)
        {
            if (id != servicio.ServiciotransporteId)
            {
                return BadRequest(new CustomResponse<ServicioTransporteDto>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID del objeto"
                });
            }

            var existente = _servicioService.GetById(id);
            if (existente == null)
            {
                return NotFound(new CustomResponse<ServicioTransporteDto>
                {
                    Success = false,
                    Message = $"No se encontró el servicio con ID {id}"
                });
            }

            existente.Nombre = servicio.Nombre;
            existente.Descripcion = servicio.Descripcion;
            existente.Email = servicio.Email;

            var actualizado = _servicioService.Update(existente);
            //_unitOfWork.Save();

            var dtoActualizado = _mapper.Map<ServicioTransporteDto>(actualizado);
            var response = new CustomResponse<ServicioTransporteDto>
            {
                Success = true,
                Message = "Servicio actualizado correctamente",
                Data = dtoActualizado
            };

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public ActionResult<CustomResponse<ServicioTransporteDto>> SetActive(int id, [FromQuery] bool active)
        {
            var servicio = _servicioService.GetById(id);
            if (servicio == null)
            {
                return NotFound(new CustomResponse<ServicioTransporteDto>
                {
                    Success = false,
                    Message = $"No se encontró el servicio con ID {id}"
                });
            }

            _servicioService.SetActive(id, active);
            //_unitOfWork.Save();

            var actualizado = _servicioService.GetById(id);
            var dtoActualizado = _mapper.Map<ServicioTransporteDto>(actualizado);
            var response = new CustomResponse<ServicioTransporteDto>
            {
                Success = true,
                Message = active ? "Servicio activado correctamente" : "Servicio desactivado correctamente",
                Data = dtoActualizado
            };

            return Ok(response);
        }
    }
}
