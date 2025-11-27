using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class StudentDocument
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; } 
        [Required]
        public string FilePath { get; set; } 

        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
