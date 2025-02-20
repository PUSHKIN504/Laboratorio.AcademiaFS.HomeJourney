using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AcademiaFS.HomeJourney.WebAPI.Utilities;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/departamentos")]
    public class DepartamentosController : Controller
    {
        private readonly IGenericServiceInterface<Departamentos, int> _departamentosService;
        private readonly IMapper _mapper;

        public DepartamentosController(
            IGenericServiceInterface<Departamentos, int> departamentosService,
            IMapper mapper)
        {
            _departamentosService = departamentosService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<DepartamentoDto>>> GetAll()
        {
            var departamentos = _departamentosService.GetAll();
            var dtoList = _mapper.Map<List<DepartamentoDto>>(departamentos);

            var response = new CustomResponse<IEnumerable<DepartamentoDto>>
            {
                Success = true,
                Message = "Listado de departamentos obtenido correctamente",
                Data = dtoList
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<DepartamentoDto>> GetById(int id)
        {
            var departamento = _departamentosService.GetById(id);
            if (departamento == null)
            {
                return NotFound(new CustomResponse<DepartamentoDto>
                {
                    Success = false,
                    Message = $"No se encontró el departamento con ID {id}"
                });
            }

            var dto = _mapper.Map<DepartamentoDto>(departamento);

            var response = new CustomResponse<DepartamentoDto>
            {
                Success = true,
                Message = "Departamento encontrado",
                Data = dto
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<CustomResponse<DepartamentoDto>> Create([FromBody] DepartamentoDto dto)
        {
            var entity = _mapper.Map<Departamentos>(dto);
            entity.Activo = true;

            var creado = _departamentosService.Create(entity);
            var dtoCreado = _mapper.Map<DepartamentoDto>(creado);

            var response = new CustomResponse<DepartamentoDto>
            {
                Success = true,
                Message = "Departamento creado correctamente",
                Data = dtoCreado
            };

            return CreatedAtAction(nameof(GetById), new { id = dtoCreado.DepartamentoId }, response);
        }

        [HttpPut("{id}")]
        public ActionResult<CustomResponse<DepartamentoDto>> Update(int id, [FromBody] DepartamentoDto dto)
        {
            if (id != dto.DepartamentoId)
            {
                return BadRequest(new CustomResponse<DepartamentoDto>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID del objeto"
                });
            }

            var departamento = _departamentosService.GetById(id);
            if (departamento == null)
            {
                return NotFound(new CustomResponse<DepartamentoDto>
                {
                    Success = false,
                    Message = $"No se encontró el departamento con ID {id}"
                });
            }

            _mapper.Map(dto, departamento);
            _departamentosService.Update(departamento);

            var dtoActualizado = _mapper.Map<DepartamentoDto>(departamento);

            var response = new CustomResponse<DepartamentoDto>
            {
                Success = true,
                Message = "Departamento actualizado correctamente",
                Data = dtoActualizado
            };

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public ActionResult<CustomResponse<DepartamentoDto>> SetActive(int id, [FromQuery] bool active)
        {
            var departamento = _departamentosService.GetById(id);
            if (departamento == null)
            {
                return NotFound(new CustomResponse<DepartamentoDto>
                {
                    Success = false,
                    Message = $"No se encontró el departamento con ID {id}"
                });
            }

            _departamentosService.SetActive(id, active);

            var departamentoActualizado = _departamentosService.GetById(id);
            var dtoActualizado = _mapper.Map<DepartamentoDto>(departamentoActualizado);

            var response = new CustomResponse<DepartamentoDto>
            {
                Success = true,
                Message = active ? "El departamento ha sido activado" : "El departamento ha sido desactivado",
                Data = dtoActualizado
            };

            return Ok(response);
        }
    }
}
