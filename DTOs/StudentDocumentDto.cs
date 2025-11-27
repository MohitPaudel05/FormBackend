using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class StudentDocumentDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string DocumentType { get; set; }

        [MaxLength(200)]
        public IFormFile? FilePath { get; set; }

    }
}
