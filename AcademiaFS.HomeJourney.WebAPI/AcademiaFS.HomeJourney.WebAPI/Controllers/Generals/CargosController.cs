using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/cargos")]
    public class CargosController : Controller
    {
        private readonly IGenericServiceInterface<Cargos, int> _cargoService;
        private readonly IMapper _mapper;

        public CargosController(IGenericServiceInterface<Cargos, int> cargoService, IMapper mapper)
        {
            _cargoService = cargoService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<CargoDto>>> GetAll()
        {
            var ciudades = _cargoService.GetAll();
            var dtoList = _mapper.Map<List<CargoDto>>(ciudades);

            var response = new CustomResponse<IEnumerable<CargoDto>>
            {
                Success = true,
                Message = "Listado de ciudades obtenido correctamente",
                Data = dtoList
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<CargoDto>> GetById(int id)
        {
            var ciudad = _cargoService.GetById(id);
            if (ciudad == null)
            {
                return NotFound(new CustomResponse<CargoDto>
                {
                    Success = false,
                    Message = $"No se encontró la ciudad con ID {id}"
                });
            }

            var dto = _mapper.Map<CargoDto>(ciudad);

            var response = new CustomResponse<CargoDto>
            {
                Success = true,
                Message = "Ciudad encontrada",
                Data = dto
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<CustomResponse<CargoDto>> Create([FromBody] CargoDto dto)
        {
            var entity = _mapper.Map<Cargos>(dto);

            var creado = _cargoService.Create(entity);
            var dtoCreado = _mapper.Map<CargoDto>(creado);

            var response = new CustomResponse<CargoDto>
            {
                Success = true,
                Message = "Ciudad creada correctamente",
                Data = dtoCreado
            };

            return CreatedAtAction(nameof(GetById), new { id = dtoCreado.CargoId }, response);
        }

        [HttpPut("{id}")]
        public ActionResult<CustomResponse<CargoDto>> Update(int id, [FromBody] CargoDto dto)
        {
            if (id != dto.CargoId)
            {
                return BadRequest(new CustomResponse<CargoDto>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID del objeto"
                });
            }

            var ciudad = _cargoService.GetById(id);
            if (ciudad == null)
            {
                return NotFound(new CustomResponse<CargoDto>
                {
                    Success = false,
                    Message = $"No se encontró la ciudad con ID {id}"
                });
            }

            _mapper.Map(dto, ciudad);
            _cargoService.Update(ciudad);

            var dtoActualizado = _mapper.Map<CargoDto>(ciudad);

            var response = new CustomResponse<CargoDto>
            {
                Success = true,
                Message = "Ciudad actualizada correctamente",
                Data = dtoActualizado
            };

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public ActionResult<CustomResponse<CargoDto>> SetActive(int id, [FromQuery] bool active)
        {
            var ciudad = _cargoService.GetById(id);
            if (ciudad == null)
            {
                return NotFound(new CustomResponse<CargoDto>
                {
                    Success = false,
                    Message = $"No se encontró la ciudad con ID {id}"
                });
            }

            _cargoService.SetActive(id, active);

            var ciudadActualizada = _cargoService.GetById(id);
            var dto = _mapper.Map<CargoDto>(ciudadActualizada);

            var response = new CustomResponse<CargoDto>
            {
                Success = true,
                Message = active ? "La ciudad ha sido activada" : "La ciudad ha sido desactivada",
                Data = dto
            };

            return Ok(response);
        }

    }
}
