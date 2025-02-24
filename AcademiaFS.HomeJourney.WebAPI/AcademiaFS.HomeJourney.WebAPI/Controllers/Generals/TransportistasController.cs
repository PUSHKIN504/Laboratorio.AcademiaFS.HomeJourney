using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/transportistas")]
    public class TransportistasController : Controller
    {
        private readonly TransportistaService _service;
        private readonly IMapper _mapper;

        public TransportistasController(TransportistaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<CustomResponse<Transportistas>>> Create([FromBody] CreateTransportistaDto dto)
        {
            try
            {
                var transportistaCreado = await _service.CreateTransportistaAsync(dto);
                var response = new CustomResponse<Transportistas>
                {
                    Success = true,
                    Message = "Transportista creado correctamente.",
                    Data = transportistaCreado
                };
                return CreatedAtAction(nameof(GetById), new { id = transportistaCreado.TransportistaId }, response);
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
        public async Task<ActionResult<CustomResponse<Transportistas>>> GetById(int id)
        {
            var transportista = await _service.GetByIdAsync(id);
            if (transportista == null)
            {
                return NotFound(new CustomResponse<string>
                {
                    Success = false,
                    Message = $"No se encontró el transportista con ID {id}."
                });
            }
            return Ok(new CustomResponse<Transportistas>
            {
                Success = true,
                Message = "Transportista encontrado.",
                Data = transportista
            });
        }
    }
}
