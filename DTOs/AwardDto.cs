using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class AwardDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string IssuingOrganization { get; set; }

        public int? YearReceived { get; set; }
        
    }
}
