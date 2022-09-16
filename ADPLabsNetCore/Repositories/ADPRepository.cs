using ADPLabsNetCore.Db;
using ADPLabsNetCore.Models;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ADPLabsNetCore.Repositories
{
    public class ADPRepository : IADPRepository
    {
        public readonly ADPContext context;

        public ADPRepository(ADPContext context) => this.context = context;

        public async Task<TaskTable> GetTask(string taskId)
        {
            var entity = await context.TaskTable.FindAsync(taskId);
            return entity;
        }

        public async Task<TaskTable> SaveTask(ADPTask task)
        {
            var entity = new TaskTable
            {
                id = task.id,
                left = task.left,
                right = task.right,
                operation = task.operation,
                message = MessagesTask.created,
                lastStatus = 200,
                taskLastUpdate = DateTime.Now
            };
            await context.TaskTable.AddAsync(entity);
            return entity;
        }

        public async Task<TaskTable> UpdateTask(string taskId, double? result, string? message, int? status)
        {
            var entity = await GetTask(taskId);

            entity.id = taskId;
            entity.result = result;
            entity.message = message;
            entity.lastStatus = status;
            entity.taskLastUpdate = DateTime.Now;

            context.TaskTable.Update(entity);
            return entity;
        }
    }
}
