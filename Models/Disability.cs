using FormBackend.Enums;

namespace FormBackend.Models
{
    public class Disability
    {
        public int Id { get; set; }
        public DisabilityStatus DisabilityStatus { get; set; }
        public string? DisabilityType { get; set; } // Only if not None
        public int? DisabilityPercentage { get; set; } // 0–100
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
