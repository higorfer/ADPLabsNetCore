using ADPLabsNetCore.Models;
using ADPLabsNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

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

        /// <summary>
        /// Retrieves an ADP task
        /// </summary>
        [HttpGet("/GetAdpTask")]
        [ProducesResponseType(typeof(ADPTask), 200)] 
        public async Task<ActionResult<ADPTask>> GetAdpTask()
        {
            var task = await _externalADPServices.GetAdpTask();
            return Ok(task);
        }

        /// <summary>
        /// Get and Process an ADP task
        /// </summary>
        [HttpGet("/ProcessTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(503)]
        [Produces("application/json")]
        public async Task<ActionResult<TaskTable>> ProcessTaskAsync()
        {
            //get current task
            var adpTask = await _externalADPServices.GetAdpTask();
            //calculate the current task
            var taskToPost = _aDPCalcService.Calculate(adpTask);
            //submit the results
            var submitedTask = await _externalADPServices.SubmitAdpTask(taskToPost);

            return (HttpStatusCode)submitedTask.lastStatus switch
            {
                HttpStatusCode.OK => Ok("Success: " + JsonSerializer.Serialize(submitedTask)), //json and options added due to pretty print into the console app
                HttpStatusCode.BadRequest => BadRequest("Incorrect value in result; No ID specified; Value is invalid"),
                HttpStatusCode.NotFound => NotFound("Value not found for specified ID"),
                HttpStatusCode.ServiceUnavailable => StatusCode(503, "Error communicating with database"),
                _ => Problem("Internal Server Error"),
            };
        }
    }
}
