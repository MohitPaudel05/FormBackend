using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class DocumentDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string DocumentType { get; set; }

        [Required, MaxLength(200)]
        public string FilePath { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
