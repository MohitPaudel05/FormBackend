using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Qualification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required, MaxLength(50)]
        public string QualificationName { get; set; } // e.g., SLC, +2, Bachelor

        [MaxLength(100)]
        public string BoardOrUniversity { get; set; }

        [MaxLength(150)]
        public string InstitutionName { get; set; }

        public int? PassedYear { get; set; }

        [MaxLength(20)]
        public string DivisionOrGPA { get; set; }

        [MaxLength(200)]
        public string MarksheetDocumentPath { get; set; }
    }
}
