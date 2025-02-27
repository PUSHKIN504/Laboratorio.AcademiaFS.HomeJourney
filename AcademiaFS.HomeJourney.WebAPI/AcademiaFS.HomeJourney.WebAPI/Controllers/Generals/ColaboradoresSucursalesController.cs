using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/colaboradoressucursales")]
    public class ColaboradoresSucursalesController : Controller
    {
        private readonly IGenericServiceInterface<Colaboradoressucursales, int> _service;
        private readonly IMapper _mapper;
        private readonly DomainServiceViaje _domainService;
        public ColaboradoresSucursalesController(IGenericServiceInterface<Colaboradoressucursales, int> service, IMapper mapper, DomainServiceViaje domainServiceViaje)
        {
            _service = service;
            _mapper = mapper;
            _domainService = domainServiceViaje;
        }

        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<ColaboradorSucursalDto>>> GetAll()
        {
            var list = _service.GetAll();
            var dtoList = _mapper.Map<IEnumerable<ColaboradorSucursalDto>>(list);
            return Ok(new CustomResponse<IEnumerable<ColaboradorSucursalDto>>
            {
                Success = true,
                Message = "Listado de colaboradores por sucursal obtenido correctamente.",
                Data = dtoList
            });
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<ColaboradorSucursalDto>> GetById(int id)
        {
            var entity = _service.GetById(id);
            if (entity == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró el registro con ID {id}."
                });
            }
            var dto = _mapper.Map<ColaboradorSucursalDto>(entity);
            return Ok(new CustomResponse<ColaboradorSucursalDto>
            {
                Success = true,
                Message = "Registro encontrado.",
                Data = dto
            });
        }

        //[HttpPost]
        //public ActionResult<CustomResponse<ColaboradorSucursalDto>> Create([FromBody] ColaboradorSucursalDto dto)
        //{
        //    var entity = _mapper.Map<Colaboradoressucursales>(dto);
        //    entity.Usuariomodifica = null;
        //    entity.Fechamodifica = null;
        //    var created = _service.Create(entity);
        //    var createdDto = _mapper.Map<ColaboradorSucursalDto>(created);
        //    return CreatedAtAction(nameof(GetById), new { id = created.ColaboradorsucursalId }, new CustomResponse<ColaboradorSucursalDto>
        //    {
        //        Success = true,
        //        Message = "Registro creado correctamente.",
        //        Data = createdDto
        //    });
        //}

        [HttpPost]
        public async Task<ActionResult<CustomResponse<ColaboradorSucursalDto>>> Create([FromBody] ColaboradorSucursalDto dto)
        {
            try
            {
                // Mapear DTO a entidad
                var entity = _mapper.Map<Colaboradoressucursales>(dto);
                entity.Usuariomodifica = null;
                entity.Fechamodifica = null;

                // Validar y establecer la distancia real con Google Maps
                await _domainService.ValidateAndSetDistanceAsync(entity);

                // Guardar usando el servicio genérico
                var created = _service.Create(entity);
                var createdDto = _mapper.Map<ColaboradorSucursalDto>(created);

                return CreatedAtAction(nameof(GetById), new { id = created.ColaboradorsucursalId }, new CustomResponse<ColaboradorSucursalDto>
                {
                    Success = true,
                    Message = "Registro creado correctamente.",
                    Data = createdDto
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new CustomResponse<ColaboradorSucursalDto>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResponse<ColaboradorSucursalDto>
                {
                    Success = false,
                    Message = $"Error al crear el registro: {ex.Message}"
                });
            }
        }
        [HttpPut("{id}")]
        public ActionResult<CustomResponse<ColaboradorSucursalDto>> Update(int id, [FromBody] ColaboradorSucursalDto dto)
        {
            if (id != dto.ColaboradorsucursalId)
            {
                return BadRequest(new CustomResponse<string>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID del objeto."
                });
            }

            var entity = _mapper.Map<Colaboradoressucursales>(dto);
            var updated = _service.Update(entity);
            var updatedDto = _mapper.Map<ColaboradorSucursalDto>(updated);
            return Ok(new CustomResponse<ColaboradorSucursalDto>
            {
                Success = true,
                Message = "Registro actualizado correctamente.",
                Data = updatedDto
            });
        }

        [HttpPatch("{id}")]
        public ActionResult<CustomResponse<ColaboradorSucursalDto>> SetActive(int id, [FromQuery] bool active)
        {
            var entity = _service.GetById(id);
            if (entity == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró el registro con ID {id}."
                });
            }

            _service.SetActive(id, active);
            var updated = _service.GetById(id);
            var updatedDto = _mapper.Map<ColaboradorSucursalDto>(updated);
            return Ok(new CustomResponse<ColaboradorSucursalDto>
            {
                Success = true,
                Message = active ? "El registro ha sido activado." : "El registro ha sido desactivado.",
                Data = updatedDto
            });
        }


    }
}
