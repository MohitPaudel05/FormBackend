using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class AcademicHistory
    {
        public int Id { get; set; }

        [Required]
        public QualificationType Qualification { get; set; }

        [Required]
        public string Board { get; set; } = string.Empty;

        [Required]
        public string Institution { get; set; } = string.Empty;

        [Required]
        public int PassedYear { get; set; }

        [Required]
        public DivisionGPA DivisionGPA { get; set; }

        public string? MarksheetPath { get; set; }
        public string? ProvisionalPath { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}