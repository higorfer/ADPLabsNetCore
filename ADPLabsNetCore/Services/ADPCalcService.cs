using ADPLabsNetCore.Models;
using ADPLabsNetCore.Repositories;
using System.Net;

namespace ADPLabsNetCore.Services
{
    public class ADPCalcService : IADPCalcService
    {

        private readonly IADPRepository _ADPRepository;

        public ADPCalcService(IADPRepository aDPRepository)
        {
            _ADPRepository = aDPRepository;
        }

        public double AdditionTask(double left, double right) { return left + right; }
        public double SubtractionTask(double left, double right) { return left - right; }
        public double MultiplicationTask(double left, double right) { return left * right; }
        public double DivisionTask(double left, double right) { return left / right; }
        public double RemainderTask(double left, double right) { return left % right; }

        public CalcBody Calculate(ADPTask adpTask)
        {
            CalcBody taskToPost = new CalcBody();
            taskToPost.id = adpTask.id;

            switch (adpTask.operation)
            {
                case Operation.addition:
                    taskToPost.result = AdditionTask(adpTask.left, adpTask.right);
                    break;
                case Operation.subtraction:
                    taskToPost.result = SubtractionTask(adpTask.left, adpTask.right);
                    break;
                case Operation.multiplication:
                    taskToPost.result = MultiplicationTask(adpTask.left, adpTask.right);
                    break;
                case Operation.division:
                    taskToPost.result = DivisionTask(adpTask.left, adpTask.right);
                    break;
                case Operation.remainder:
                    taskToPost.result = RemainderTask(adpTask.left, adpTask.right);
                    break;
            }

            _ADPRepository.UpdateTask(adpTask.id, taskToPost.result, MessagesTask.calculated, (int?)HttpStatusCode.OK);
            return taskToPost;
        }
    }
}
