using ADPLabsNetCore.Models;
using System.Net;

namespace ADPLabsNetCore.Services
{
    public interface IExternalADPServices
    {
        Task<ADPTask> GetAdpTask();
        Task<HttpStatusCode> SubmitAdpTask(CalcBody calcBody);
    }
}
