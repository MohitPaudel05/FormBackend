using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Achievement
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string IssuingOrganization { get; set; } = string.Empty;

        [Required]
        public int YearReceived { get; set; }

        // Link to Student
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
