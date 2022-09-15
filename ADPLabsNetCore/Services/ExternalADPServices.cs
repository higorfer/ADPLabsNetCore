using ADPLabsNetCore.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ADPLabsNetCore.Services
{
    public class ExternalADPServices : IExternalADPServices
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public ExternalADPServices(IADPCalcService aDPCalcService, IConfiguration iConfig)
        {
            //Dependency injection in order to use appsettings.json
            _configuration = iConfig;
            string protocol = _configuration.GetValue<string>("ADP:Protocol");
            string baseUrl = _configuration.GetValue<string>("ADP:BaseUrl");
            _url = protocol + baseUrl;
        }

        public async Task<ADPTask> GetAdpTask()
        {

            string urlGetTask = _url + _configuration.GetValue<string>("ADP:GetTask");
            var adpTask = new ADPTask();

            using (var httpClient = new HttpClient())
            {
                
                //Perform GetAsync request
                using (var response = await httpClient.GetAsync(urlGetTask))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    //Adding options for enum conversion
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());

                    adpTask = JsonSerializer.Deserialize<ADPTask>(apiResponse, options);
                }
            }

            return adpTask;

        }

        public async Task<HttpStatusCode> SubmitAdpTask(CalcBody calcBody)
        {

            string urlSubmitTask = _url + _configuration.GetValue<string>("ADP:SubmitTask");

            //creating body
            var requestData = JsonSerializer.Serialize(calcBody);
            var body = new StringContent(requestData, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                //Perform postAsync request
                using (var response = await httpClient.PostAsync(urlSubmitTask, body))
                {
                    return response.StatusCode;
                }
            }
        }

    }
}
