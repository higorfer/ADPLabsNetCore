using System.ComponentModel.DataAnnotations;

namespace ADPLabsNetCore.Models
{
    public class ADPTask
    {
        public string? id { get; set; }
        public Operation operation { get; set; }
        public double left { get; set; }
        public double right { get; set; }
    }

    public enum Operation
    {
        addition,
        subtraction,
        multiplication,
        division,
        remainder
    }

    internal static class MessagesTask
    {
        public const string created = "created";
        public const string calculated = "calculated";
        public const string submited = "submited";
    }
}
