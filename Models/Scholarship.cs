using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Scholarship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required, MaxLength(50)]
        public string ScholarshipType { get; set; } // e.g., Government, Institutional, Private

        [MaxLength(100)]
        public string ProviderName { get; set; }
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
    }
}
