using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models.Lookup
{
    public class Ethnicity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}
