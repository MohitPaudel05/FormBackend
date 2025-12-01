using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class StudentExtraInfo
    {
        public int Id { get; set; }

        // Interests as comma-separated string or JSON
        public string ExtracurricularInterests { get; set; } = string.Empty;

        // If "Other" interest is specified
        public string? OtherInterest { get; set; }

        [Required]
        public HostellerStatus HostellerStatus { get; set; }

        [Required]
        public TransportationMethod Transportation { get; set; }

        // Link to Student
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
