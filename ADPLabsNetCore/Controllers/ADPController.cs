using ADPLabsNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ADPLabsNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ADPController : Controller
    {
        private IConfiguration configuration;
        private readonly string url;
        public ADPController(IConfiguration iConfig)
        {
            configuration = iConfig;
            string protocol = configuration.GetValue<string>("ADP:Protocol");
            string baseUrl = configuration.GetValue<string>("ADP:BaseUrl");
            url = protocol + baseUrl;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ADPTask), 200)] 
        public async Task<IActionResult> GetAdpTask()
        {

            string urlGetTask = url + configuration.GetValue<string>("ADP:GetTask");

            var adpTask = new ADPTask();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(urlGetTask))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    adpTask = JsonSerializer.Deserialize<ADPTask>(apiResponse, options);
                }
            }
            return Ok(adpTask);
        }
    }
}
