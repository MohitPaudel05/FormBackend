using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Declaration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required]
        public bool IsDeclared { get; set; } // Checkbox confirmation

        [Required]
        public DateTime DateOfApplication { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string Place { get; set; }
    }
}
