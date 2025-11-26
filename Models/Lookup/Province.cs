using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models.Lookup
{
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
