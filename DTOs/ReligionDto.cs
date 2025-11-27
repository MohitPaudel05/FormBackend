using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class ReligionDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string ReligionName { get; set; }

        [Required, MaxLength(50)]
        public string Ethnicity { get; set; }
    }
}
