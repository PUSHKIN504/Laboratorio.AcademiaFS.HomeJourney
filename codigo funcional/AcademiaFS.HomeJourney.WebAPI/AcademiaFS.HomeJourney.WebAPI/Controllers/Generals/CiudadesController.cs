﻿using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AcademiaFS.HomeJourney.WebAPI.Utilities;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/ciudades")]
    public class CiudadesController : Controller
    {
        private readonly IGenericServiceInterface<Ciudades, int> _ciudadesService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CiudadesController(IGenericServiceInterface<Ciudades, int> ciudadesService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _ciudadesService = ciudadesService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<CiudadesDto>>> GetAll()
        {
            var ciudades = _ciudadesService.GetAll();
            var dtoList = _mapper.Map<List<CiudadesDto>>(ciudades);

            var response = new CustomResponse<IEnumerable<CiudadesDto>>
            {
                Success = true,
                Message = "Listado de ciudades obtenido correctamente",
                Data = dtoList
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<CiudadesDto>> GetById(int id)
        {
            var ciudad = _ciudadesService.GetById(id);
            if (ciudad == null)
            {
                return NotFound(new CustomResponse<CiudadesDto>
                {
                    Success = false,
                    Message = $"No se encontró la ciudad con ID {id}"
                });
            }

            var dto = _mapper.Map<CiudadesDto>(ciudad);

            var response = new CustomResponse<CiudadesDto>
            {
                Success = true,
                Message = "Ciudad encontrada",
                Data = dto
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<CustomResponse<CiudadesDto>> Create([FromBody] CiudadesDto dto)
        {
            var entity = _mapper.Map<Ciudades>(dto);
            entity.Activo = true;

            _ciudadesService.Create(entity);
            //_unitOfWork.Save();

            var dtoCreado = _mapper.Map<CiudadesDto>(entity);
            var response = new CustomResponse<CiudadesDto>
            {
                Success = true,
                Message = "Ciudad creada correctamente",
                Data = dtoCreado
            };

            return CreatedAtAction(nameof(GetById), new { id = dtoCreado.CiudadId }, response);
        }

        [HttpPut("{id}")]
        public ActionResult<CustomResponse<CiudadesDto>> Update(int id, [FromBody] CiudadesDto dto)
        {
            if (id != dto.CiudadId)
            {
                return BadRequest(new CustomResponse<CiudadesDto>
                {
                    Success = false,
                    Message = "El ID de la ruta no coincide con el ID del objeto"
                });
            }

            var ciudad = _ciudadesService.GetById(id);
            if (ciudad == null)
            {
                return NotFound(new CustomResponse<CiudadesDto>
                {
                    Success = false,
                    Message = $"No se encontró la ciudad con ID {id}"
                });
            }

            _mapper.Map(dto, ciudad);
            _ciudadesService.Update(ciudad);
            //_unitOfWork.Save();

            var dtoActualizado = _mapper.Map<CiudadesDto>(ciudad);

            var response = new CustomResponse<CiudadesDto>
            {
                Success = true,
                Message = "Ciudad actualizada correctamente",
                Data = dtoActualizado
            };

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public ActionResult<CustomResponse<CiudadesDto>> SetActive(int id, [FromQuery] bool active)
        {
            var ciudad = _ciudadesService.GetById(id);
            if (ciudad == null)
            {
                return NotFound(new CustomResponse<CiudadesDto>
                {
                    Success = false,
                    Message = $"No se encontró la ciudad con ID {id}"
                });
            }

            _ciudadesService.SetActive(id, active);
            //_unitOfWork.Save();

            var ciudadActualizada = _ciudadesService.GetById(id);
            var dtoActualizado = _mapper.Map<CiudadesDto>(ciudadActualizada);

            var response = new CustomResponse<CiudadesDto>
            {
                Success = true,
                Message = active ? "La ciudad ha sido activada" : "La ciudad ha sido desactivada",
                Data = dtoActualizado
            };

            return Ok(response);
        }
    }
}
