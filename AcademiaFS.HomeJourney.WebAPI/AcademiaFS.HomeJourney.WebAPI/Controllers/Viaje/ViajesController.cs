using AcademiaFS.HomeJourney.WebAPI._Features.Viaje;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Viaje
{
    [ApiController]
    [Route("academiafarsiman/viajes")]
    public class ViajesController : Controller
    {
        private readonly ViajesService _viajesService;
        private readonly IMapper _mapper;

        public ViajesController(ViajesService viajesService, IMapper mapper)
        {
            _viajesService = viajesService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CustomResponse<Viajes>>> CreateViaje([FromBody] ViajesCreateDto dto)
        {
            var viaje = _mapper.Map<Viajes>(dto);
            viaje.Activo = true;
            viaje.Fechacrea = System.DateTime.Now;

            var detalles = _mapper.Map<List<Viajesdetalles>>(dto.Detalles);
            foreach (var detalle in detalles)
            {
                detalle.Activo = true;
                detalle.Fechacrea = System.DateTime.Now;
            }
              
            var viajeCreado = await _viajesService.CreateViajeWithDetailsAsync(viaje, detalles);

            var response = new CustomResponse<Viajes>
            {
                Success = true,
                Message = "Viaje y detalles creados correctamente",
                Data = viajeCreado
            };

            return CreatedAtAction(nameof(GetById), new { id = viajeCreado.ViajeId }, response);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<Viajes>> GetById(int id)
        {
            return Ok();
        }
    }
}
