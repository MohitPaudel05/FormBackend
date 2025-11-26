using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class DeclarationDto
    {
        public int Id { get; set; }

        [Required]
        public bool IsDeclared { get; set; }

        [Required]
        public DateTime DateOfApplication { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string Place { get; set; }
    }
}
