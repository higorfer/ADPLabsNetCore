using ADPLabsNetCore.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;

namespace ADPLabsNetCore.Services
{
    public class ExternalADPServices : IExternalADPServices
    {
        private readonly IConfiguration _configuration;
        private readonly string url;

        public ExternalADPServices(IADPCalcService aDPCalcService, IConfiguration iConfig)
        {
            _configuration = iConfig;
            string protocol = _configuration.GetValue<string>("ADP:Protocol");
            string baseUrl = _configuration.GetValue<string>("ADP:BaseUrl");
            url = protocol + baseUrl;
        }

        public async Task<ADPTask> GetAdpTask()
        {

            string urlGetTask = url + _configuration.GetValue<string>("ADP:GetTask");
            var adpTask = new ADPTask();

            try
            {

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

            return adpTask;

            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw ex;
            }
        }

    }
}
