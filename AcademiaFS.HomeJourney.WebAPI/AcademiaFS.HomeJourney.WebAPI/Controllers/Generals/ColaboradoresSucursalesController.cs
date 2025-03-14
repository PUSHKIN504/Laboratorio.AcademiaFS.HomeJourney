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
    public class ColaboradoresSucursalesController : ControllerBase
    {
        private readonly IGenericServiceInterface<Colaboradoressucursales, int> _service;
        private readonly IMapper _mapper;
        private readonly ViajesService _domainService;
        private readonly IGenericServiceInterface<Colaboradores, int> _colaboradorService;
        private readonly IGenericServiceInterface<Personas, int> _personaService;
        private readonly IGenericServiceInterface<Sucursales, int> _sucursalService;
        public ColaboradoresSucursalesController(IGenericServiceInterface<Colaboradoressucursales, int> service, IMapper mapper, ViajesService domainServiceViaje, IGenericServiceInterface<Colaboradores, int> colaboradorService, IGenericServiceInterface<Personas, int> personaService,
            IGenericServiceInterface<Sucursales, int> sucursalService)
        {
            _service = service;
            _mapper = mapper;
            _domainService = domainServiceViaje;
            _personaService = personaService;
            _colaboradorService = colaboradorService;
            _sucursalService = sucursalService;
        }

        //[HttpGet]
        //public ActionResult<CustomResponse<IEnumerable<ColaboradorSucursalDto>>> GetAll()
        //{
        //    var list = _service.GetAll();
        //    var dtoList = _mapper.Map<IEnumerable<ColaboradorSucursalDto>>(list);
        //    return Ok(new CustomResponse<IEnumerable<ColaboradorSucursalDto>>
        //    {
        //        Success = true,
        //        Message = "Listado de colaboradores por sucursal obtenido correctamente.",
        //        Data = dtoList
        //    });
        //}
        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<ColaboradorSucursalDto>>> GetAll()
        {
            var list = _service.GetAll();

            var colaboradores = _colaboradorService.GetAll();
            var sucursales = _sucursalService.GetAll();
            var personas = _personaService.GetAll();

            var dtoList = list.Select(entity =>
            {
                var dto = _mapper.Map<ColaboradorSucursalDto>(entity);

                var collab = colaboradores.FirstOrDefault(c => c.ColaboradorId == entity.ColaboradorId);
                if (collab != null)
                {
                    var persona = personas.FirstOrDefault(p => p.PersonaId == collab.PersonaId);
                    if (persona != null)
                    {
                        dto.NombreColaborador = $"{persona.Nombre} {persona.Apelllido}";
                    }
                    else
                    {
                        dto.NombreColaborador = "Nombre no disponible";
                    }
                }
                else
                {
                    dto.NombreColaborador = "Nombre no disponible";
                }

                var sucursal = sucursales.FirstOrDefault(s => s.SucursalId == entity.SucursalId);
                dto.NombreSucursal = sucursal?.Nombre ?? "Sucursal no disponible";
                dto.Latitud = sucursal?.Latitud ?? 0;
                dto.Longitud = sucursal?.Longitud ?? 0;
                return dto;
            });

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


        [HttpPost]
        public async Task<ActionResult<CustomResponse<ColaboradorSucursalRequestDto>>> Create([FromBody] ColaboradorSucursalRequestDto dto)
        {
            try
            {
                var entity = _mapper.Map<Colaboradoressucursales>(dto);
                entity.Usuariomodifica = null;
                entity.Fechamodifica = null;

                await _domainService.AsignarColaboradorASucursalAsync(entity);

                var created = _service.Create(entity);
                var createdDto = _mapper.Map<ColaboradorSucursalRequestDto>(created);

                return CreatedAtAction(nameof(GetById), new { id = created.ColaboradorsucursalId }, new CustomResponse<ColaboradorSucursalRequestDto>
                {
                    Success = true,
                    Message = "Registro creado correctamente.",
                    Data = createdDto
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new CustomResponse<ColaboradorSucursalRequestDto>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResponse<ColaboradorSucursalRequestDto>
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
