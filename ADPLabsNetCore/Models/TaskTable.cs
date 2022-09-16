using System.ComponentModel.DataAnnotations;

namespace ADPLabsNetCore.Models
{
    public class TaskTable
    {
        [Key]
        public string? id { get; set; }
        public Operation operation { get; set; }
        public double left { get; set; }
        public double right { get; set; }
        public double? result { get; set; }
        public string? message { get; set; }
        public DateTime taskLastUpdate { get; set; }
        public int? lastStatus { get; set; }
    }
}
