using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormBackend.Models
{
    public class Scholarship
    {
        public int Id { get; set; }

        

        [Required]
        public FeeCategory FeeCategory { get; set; }

        public ScholarshipType? ScholarshipType { get; set; }
        public string? ScholarshipProvider { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ScholarshipAmount { get; set; }

        // Link with Student
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        
    }
}
