using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models.Lookup
{
    public class District
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}
