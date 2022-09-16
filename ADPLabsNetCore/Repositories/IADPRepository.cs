using ADPLabsNetCore.Db;
using ADPLabsNetCore.Models;

namespace ADPLabsNetCore.Repositories
{
    public interface IADPRepository
    {
        Task<TaskTable> GetTask(string taskId);
        Task<TaskTable> SaveTask(ADPTask task);
        Task<TaskTable> UpdateTask(string taskId, double? result, string? message, int? status);
    }
}
