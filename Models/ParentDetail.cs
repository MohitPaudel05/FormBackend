using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class ParentDetail
    {
        public int Id { get; set; }

        [Required]
        public ParentType ParentType { get; set; }  // Father, Mother, Guardian

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Occupation { get; set; } = string.Empty;

        [Required]
        public string Designation { get; set; } = string.Empty;

        [Required]
        public string Organization { get; set; } = string.Empty;

        [Required]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;


        [Required]
        public AnnualIncome FamilyIncome { get; set; }

        // Link with Student
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}

