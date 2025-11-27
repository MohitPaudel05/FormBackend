using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Disability
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }
        public bool HasDisability { get; set; } = false;

        [MaxLength(100)]
        public DisabilityType DisabilityType { get; set; } = DisabilityType.None;

        public int? DisabilityPercentage { get; set; }
    }
}
