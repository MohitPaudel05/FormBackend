using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class StudentInterest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required, MaxLength(50)]
        public string Interest { get; set; } 

        [MaxLength(100)]
        public string OtherDetail { get; set; } 
    }
}
