using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/estados")]
    public class EstadosController : Controller
    {
        private readonly IGenericServiceInterface<Estados, int> _estadosService;
        private readonly IMapper _mapper;

        public EstadosController(
            IGenericServiceInterface<Estados, int> estadosService,
            IMapper mapper)
        {
            _estadosService = estadosService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<EstadoDto>>> GetAll()
        {
            var estados = _estadosService.GetAll();
            var dtoList = _mapper.Map<List<EstadoDto>>(estados);

            var response = new CustomResponse<IEnumerable<EstadoDto>>
            {
                Success = true,
                Message = "Listado de estados obtenido correctamente",
                Data = dtoList
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<EstadoDto>> GetById(int id)
        {
            var estado = _estadosService.GetById(id);
            if (estado == null)
            {
                return NotFound(new CustomResponse<EstadoDto>
                {
                    Success = false,
                    Message = $"No se encontró el estado con ID {id}"
                });
            }

            var dto = _mapper.Map<EstadoDto>(estado);
            var response = new CustomResponse<EstadoDto>
            {
                Success = true,
                Message = "Estado encontrado",
                Data = dto
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<CustomResponse<EstadoDto>> Create([FromBody] EstadoDto dto)
        {
            var entity = _mapper.Map<Estados>(dto);

            var creado = _estadosService.Create(entity);
            var dtoCreado = _mapper.Map<EstadoDto>(creado);

            var response = new CustomResponse<EstadoDto>
            {
                Success = true,
                Message = "Estado creado correctamente",
                Data = dtoCreado
            };

            return CreatedAtAction(nameof(GetById), new { id = dtoCreado.EstadoId }, response);
        }

        [HttpPut("{id}")]
        public ActionResult<CustomResponse<EstadoDto>> Update(int id, [FromBody] EstadoDto dto)
        {
            if (id != dto.EstadoId)
            {
                return BadRequest(new CustomResponse<EstadoDto>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID del objeto"
                });
            }

            var estado = _estadosService.GetById(id);
            if (estado == null)
            {
                return NotFound(new CustomResponse<EstadoDto>
                {
                    Success = false,
                    Message = $"No se encontró el estado con ID {id}"
                });
            }

            _mapper.Map(dto, estado);

            var actualizado = _estadosService.Update(estado);
            var dtoActualizado = _mapper.Map<EstadoDto>(actualizado);

            var response = new CustomResponse<EstadoDto>
            {
                Success = true,
                Message = "Estado actualizado correctamente",
                Data = dtoActualizado
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<CustomResponse<string>> Delete(int id)
        {
            var estado = _estadosService.GetById(id);
            if (estado == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró el estado con ID {id}"
                });
            }

            _estadosService.Delete(id);

            var response = new CustomResponse<string>
            {
                Success = true,
                Message = "Estado eliminado correctamente"
            };

            return Ok(response);
        }
    }
}
