using System.ComponentModel.DataAnnotations;

namespace ADPLabsNetCore.Models
{
    public class ADPTask
    {
        [Required]
        public string? id { get; set; }
        [Required]
        public Operation operation { get; set; }
        [Required]
        public double left { get; set; }
        [Required]
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
}
