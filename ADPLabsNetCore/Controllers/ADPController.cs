using ADPLabsNetCore.Models;
using ADPLabsNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ADPLabsNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ADPController : Controller
    {
        private readonly IExternalADPServices _externalADPServices;
        private readonly IADPCalcService _aDPCalcService;
        public ADPController(IExternalADPServices externalADPServices, IADPCalcService aDPCalcService)
        {
            _externalADPServices = externalADPServices;
            _aDPCalcService = aDPCalcService;
        }


        [HttpGet("/GetAdpTask")]
        [ProducesResponseType(typeof(ADPTask), 200)] 
        public async Task<ActionResult<ADPTask>> GetAdpTask()
        {
            var task = await _externalADPServices.GetAdpTask();
            return Ok(task);
        }

        [HttpGet("/ProcessTask")]
        [ProducesResponseType(typeof(ADPTask), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> ProcessTaskAsync()
        {
            var adpTask = await _externalADPServices.GetAdpTask();
            var taskToPost = _aDPCalcService.Calculate(adpTask);

            return Ok(taskToPost);

        }
    }
}
