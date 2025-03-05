using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/estadosciviles")]
    public class EstadosCivilesController : Controller
    {
        private readonly IGenericServiceInterface<Estadosciviles, int> _estadosCivilesService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstadosCivilesController(
            IGenericServiceInterface<Estadosciviles, int> estadosCivilesService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _estadosCivilesService = estadosCivilesService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<EstadoCivilDto>>> GetAll()
        {
            var estadosCiviles = _estadosCivilesService.GetAll();
            var dtoList = _mapper.Map<List<EstadoCivilDto>>(estadosCiviles);

            var response = new CustomResponse<IEnumerable<EstadoCivilDto>>
            {
                Success = true,
                Message = "Listado de estados civiles obtenido correctamente",
                Data = dtoList
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<EstadoCivilDto>> GetById(int id)
        {
            var estadoCivil = _estadosCivilesService.GetById(id);
            if (estadoCivil == null)
            {
                return NotFound(new CustomResponse<EstadoCivilDto>
                {
                    Success = false,
                    Message = $"No se encontró el estado civil con ID {id}"
                });
            }

            var dto = _mapper.Map<EstadoCivilDto>(estadoCivil);
            var response = new CustomResponse<EstadoCivilDto>
            {
                Success = true,
                Message = "Estado civil encontrado",
                Data = dto
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<CustomResponse<EstadoCivilDto>> Create([FromBody] EstadoCivilDto dto)
        {
            var entity = _mapper.Map<Estadosciviles>(dto);
            var creado = _estadosCivilesService.Create(entity);

            var dtoCreado = _mapper.Map<EstadoCivilDto>(creado);
            var response = new CustomResponse<EstadoCivilDto>
            {
                Success = true,
                Message = "Estado civil creado correctamente",
                Data = dtoCreado
            };

            return CreatedAtAction(nameof(GetById), new { id = dtoCreado.EstadocivilId }, response);
        }

        [HttpPut("{id}")]
        public ActionResult<CustomResponse<EstadoCivilDto>> Update(int id, [FromBody] EstadoCivilDto dto)
        {
            if (id != dto.EstadocivilId)
            {
                return BadRequest(new CustomResponse<EstadoCivilDto>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID del objeto"
                });
            }

            var estadoCivil = _estadosCivilesService.GetById(id);
            if (estadoCivil == null)
            {
                return NotFound(new CustomResponse<EstadoCivilDto>
                {
                    Success = false,
                    Message = $"No se encontró el estado civil con ID {id}"
                });
            }

            _mapper.Map(dto, estadoCivil);

            var actualizado = _estadosCivilesService.Update(estadoCivil);
            _unitOfWork.Save();

            var dtoActualizado = _mapper.Map<EstadoCivilDto>(actualizado);

            var response = new CustomResponse<EstadoCivilDto>
            {
                Success = true,
                Message = "Estado civil actualizado correctamente",
                Data = dtoActualizado
            };

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public ActionResult<CustomResponse<string>> SetActive(int id, [FromQuery] bool active)
        {
            var estadoCivil = _estadosCivilesService.GetById(id);
            if (estadoCivil == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró el estado civil con ID {id}"
                });
            }

            _estadosCivilesService.SetActive(id, active);
            _unitOfWork.Save();

            var estado = active ? "activado" : "desactivado";

            return Ok(new CustomResponse<string>
            {
                Success = true,
                Message = $"Estado civil {estado} correctamente"
            });
        }
    }
}
