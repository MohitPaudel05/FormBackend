using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class CitizenshipInfo
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [MaxLength(50)]
        public string CitizenshipNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        [MaxLength(100)]
        public string IssueDistrict { get; set; }
    }
}
