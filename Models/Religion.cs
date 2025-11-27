using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Religion
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [MaxLength(50)]
        public string ReligionName { get; set; }

        [Required, MaxLength(50)]
        public string Ethnicity { get; set; }
    }
}
