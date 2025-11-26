using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required, MaxLength(50)]
        public string DocumentType { get; set; } 
        [Required, MaxLength(200)]
        public string FilePath { get; set; } 

        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
