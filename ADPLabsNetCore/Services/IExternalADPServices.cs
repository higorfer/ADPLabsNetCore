using ADPLabsNetCore.Models;

namespace ADPLabsNetCore.Services
{
    public interface IExternalADPServices
    {
        Task<ADPTask> GetAdpTask();
    }
}
