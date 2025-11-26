using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class TemporaryAddressDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Province { get; set; }

        [Required, MaxLength(50)]
        public string District { get; set; }

        [Required, MaxLength(100)]
        public string Municipality { get; set; }

        [Required, MaxLength(10)]
        public string WardNumber { get; set; }

        [MaxLength(200)]
        public string Tole { get; set; }

        [MaxLength(50)]
        public string HouseNumber { get; set; }
    }
}
