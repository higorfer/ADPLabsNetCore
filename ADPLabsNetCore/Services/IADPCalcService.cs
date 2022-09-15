using ADPLabsNetCore.Models;

namespace ADPLabsNetCore.Services
{
    public interface IADPCalcService
    {
        double AdditionTask(double left, double right);
        double SubtractionTask(double left, double right);
        double MultiplicationTask(double left, double right);
        double DivisionTask(double left, double right);
        double RemainderTask(double left, double right);
        CalcBody Calculate(ADPTask adpTask);

    }
}
