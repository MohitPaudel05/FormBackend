using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class QualificationDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string QualificationName { get; set; }

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
