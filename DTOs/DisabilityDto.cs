using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class DisabilityDto
    {
        public int Id { get; set; }
        [Required]
        public DisabilityType DisabilityType { get; set; } = DisabilityType.None;
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
