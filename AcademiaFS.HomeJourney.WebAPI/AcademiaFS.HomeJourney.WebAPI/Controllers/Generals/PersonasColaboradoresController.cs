using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/personascolaboradores")]
    public class PersonasColaboradoresController : Controller
    {
        private readonly PersonasColaboradoresService _service;
        private readonly IMapper _mapper;

        public PersonasColaboradoresController(PersonasColaboradoresService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<CustomResponse<Personas>>> Create([FromBody] CreatePersonaColaboradorDto dto)
        {
            try
            {
                var personaCreada = await _service.CreatePersonaColaboradorAsync(dto);
                var response = new CustomResponse<Personas>
                {
                    Success = true,
                    Message = "Persona y Colaborador creados correctamente.",
                    Data = personaCreada
                };
                return CreatedAtAction(nameof(GetById), new { id = personaCreada.PersonaId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"Error al crear: {ex.Message}"
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResponse<Personas>>> GetById(int id)
        {
            var persona = await _service.GetByIdAsync(id);
            var dto = _mapper.Map<PersonaDto>(persona);

            if (dto == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró la persona con ID {id}."
                });
            }
            return Ok(new CustomResponse<PersonaDto>
            {
                Success = true,
                Message = "Persona encontrada.",
                Data = dto
            });
        }
        [HttpGet]
        public async Task<ActionResult<CustomResponse<List<ColaboradorGetAllDto>>>> GetAllColaboradores()
        {
            try
            {
                var colaboradores = await _service.GetAllColaboradoresAsync();
                var colaboradoresDto = _mapper.Map<List<ColaboradorGetAllDto>>(colaboradores);

                return Ok(new CustomResponse<List<ColaboradorGetAllDto>>
                {
                    Success = true,
                    Message = "Colaboradores obtenidos correctamente. Excluye los que están en viajes hoy.",
                    Data = colaboradoresDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResponse<List<ColaboradorGetAllDto>>
                {
                    Success = false,
                    Message = $"Error al obtener los colaboradores: {ex.Message}"
                });
            }
        }
    }
}
