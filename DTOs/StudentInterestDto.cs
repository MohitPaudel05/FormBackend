using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class StudentInterestDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Interest { get; set; }

        [MaxLength(100)]
        public string OtherDetail { get; set; }
    }
}
