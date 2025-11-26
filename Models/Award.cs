using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Award
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string IssuingOrganization { get; set; }

        public int? YearReceived { get; set; }
    }
}
