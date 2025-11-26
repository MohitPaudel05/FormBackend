using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class FeeDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required, MaxLength(50)]
        public string FeeCategory { get; set; }
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
    }
}
