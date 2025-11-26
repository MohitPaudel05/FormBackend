using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models.Lookup
{
    public class BloodGroup
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(5)]
        public string Name { get; set; }
    }
}
