using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/paises")]
    public class PaisesController : Controller
    {

        private readonly IGenericServiceInterface<Paises, int> _paisesService;
        private readonly IMapper _mapper;

        public PaisesController(IGenericServiceInterface<Paises, int> paisesService, IMapper mapper)
        {
            _paisesService = paisesService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PaisesDto>> GetAll()
        {
            var paises = _paisesService.GetAll();
            var dto = _mapper.Map<List<PaisesDto>>(paises);

            var response = new CustomResponse<IEnumerable<PaisesDto>>
            {
                Success = true,
                Message = "Listado de países obtenido correctamente",
                Data = dto
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<PaisesDto> GetById(int id)
        {
            var pais = _paisesService.GetById(id);
            if (pais == null)
            {
                return NotFound(new CustomResponse<PaisesDto>
                {
                    Success = false,
                    Message = $"No se encontró el país con ID {id}"
                });
            }

            var dto = _mapper.Map<PaisesDto>(pais);

            var response = new CustomResponse<PaisesDto>
            {
                Success = true,
                Message = "País encontrado",
                Data = dto
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<PaisesDto> Create([FromBody] PaisesDto paisDto)
        {
            var entity = _mapper.Map<Paises>(paisDto);
            entity.Activo = true;

            var creado = _paisesService.Create(entity);
            var dtoCreado = _mapper.Map<PaisesDto>(creado);

            var response = new CustomResponse<PaisesDto>
            {
                Success = true,
                Message = "País creado correctamente",
                Data = dtoCreado
            };

            return CreatedAtAction(nameof(GetById), new { id = dtoCreado.PaisId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PaisesDto paisDto)
        {
            if (id != paisDto.PaisId)
            {
                return BadRequest(new CustomResponse<PaisesDto>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID del objeto"
                });
            }

            var entity = _paisesService.GetById(id);
            if (entity == null)
            {
                return NotFound(new CustomResponse<PaisesDto>
                {
                    Success = false,
                    Message = $"No se encontró el país con ID {id}"
                });
            }

            _mapper.Map(paisDto, entity);
            _paisesService.Update(entity);

            var dtoActualizado = _mapper.Map<PaisesDto>(entity);

            var response = new CustomResponse<PaisesDto>
            {
                Success = true,
                Message = "País actualizado correctamente",
                Data = dtoActualizado
            };

            return Ok(response);
        }

        [HttpPatch("{id}/activo")]
        public IActionResult SetActive(int id, [FromQuery] bool active)
        {
            var pais = _paisesService.GetById(id);
            if (pais == null)
            {
                return NotFound(new CustomResponse<PaisesDto>
                {
                    Success = false,
                    Message = $"No se encontró el país con ID {id}"
                });
            }

            _paisesService.SetActive(id, active);
            var updatedPais = _paisesService.GetById(id);
            var dto = _mapper.Map<PaisesDto>(updatedPais);

            var response = new CustomResponse<PaisesDto>
            {
                Success = true,
                Message = active ? "El país ha sido activado" : "El país ha sido desactivado",
                Data = dto
            };

            return Ok(response);
        }
    }
}
