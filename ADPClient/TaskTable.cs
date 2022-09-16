

namespace ADPClient
{
    internal class TaskTable
    {
        public string? id { get; set; }
        public int operation { get; set; }
        public double left { get; set; }
        public double right { get; set; }
        public double result { get; set; }
        public string message { get; set; }
        public DateTime taskLastUpdate { get; set; }
        public int lastStatus { get; set; }
    }

    public enum Operation
    {
        addition,
        subtraction,
        multiplication,
        division,
        remainder
    }
}
