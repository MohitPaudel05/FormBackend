using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class HostelTransportDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required, MaxLength(20)]
        public string ResidencyType { get; set; }

        [MaxLength(50)]
        public string TransportMethod { get; set; } 
    }
}
