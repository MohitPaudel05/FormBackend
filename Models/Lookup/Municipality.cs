using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models.Lookup
{
    public class Municipality
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}
