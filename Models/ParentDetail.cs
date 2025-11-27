using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class ParentDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required]
        public ParentType Relation { get; set; } // Father, Mother, Other

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(100)]
        public string Occupation { get; set; }

        [MaxLength(100)]
        public string Designation { get; set; }

        [MaxLength(150)]
        public string Organization { get; set; }

        [Required, MaxLength(15)]
        public string MobileNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string AnnualFamilyIncome { get; set; } // Store directly as string
    }
}
 