using AcademiaFS.HomeJourney.WebAPI._Features.Generals;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio.Academina.JasonVillanueva.WebAPI.Controllers
{
    [Route("academiafs/paises")]
    public class CommonController : ControllerBase
    {
        private readonly CommonService _commonService;
        private readonly PaisesService _paisesService;
        public CommonController (CommonService commonService, PaisesService paisesService)
        {
            _commonService = commonService;
            _paisesService = paisesService;
        }

        [HttpGet]
        public IActionResult GetListadoPaises()
        {
            var result = _commonService.ListadoPaises();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<PaisesDto> CrearPais([FromBody] PaisesDto paisDto)
        {
            if (paisDto == null)
            {
                return BadRequest("El país es nulo");
            }
            var creado = _paisesService.CrearPais(paisDto);
            return CreatedAtAction(nameof(GetListadoPaises), new { id = creado.PaisId }, creado);
        }

        // PUT: api/paises/{id}
        [HttpPut("{id}")]
        public ActionResult<PaisesDto> EditarPais(int id, [FromBody] PaisesDto paisDto)
        {
            if (paisDto == null || paisDto.PaisId != id)
            {
                return BadRequest("El ID del país no coincide");
            }
            try
            {
                var actualizado = _paisesService.EditarPais(paisDto);
                return Ok(actualizado);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
