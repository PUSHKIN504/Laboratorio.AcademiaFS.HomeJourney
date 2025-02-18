using Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio.Academina.JasonVillanueva.WebAPI.Controllers
{
    [Route("api/controllers")]
    public class CommonController : ControllerBase
    {
        private readonly CommonService _commonService;
        public CommonController (CommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("ListadoGerentes")]
        public IActionResult GetListadoAgentes()
        {
            var result = _commonService.ListadoUsuarios();

            return Ok(result);
        }
    }
}
