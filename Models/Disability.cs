using FormBackend.Enums;

namespace FormBackend.Models
{
    public class Disability
    {
        public int Id { get; set; }
        public DisabilityStatus DisabilityStatus { get; set; }
       
        public int? DisabilityPercentage { get; set; } // 0–100
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
