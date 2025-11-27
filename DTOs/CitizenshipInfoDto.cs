using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class CitizenshipInfoDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string CitizenshipNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        [MaxLength(100)]
        public string IssueDistrict { get; set; }
    }
}
