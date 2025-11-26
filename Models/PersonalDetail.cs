using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class PersonalDetail
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(100)]
        public string PlaceOfBirth { get; set; }

        [Required, MaxLength(50)]
        public string Nationality { get; set; } = "Nepali";

        [MaxLength(50)]
        public string CitizenshipNumber { get; set; }

        public DateTime? CitizenshipIssueDate { get; set; }

        [MaxLength(100)]
        public string CitizenshipIssueDistrict { get; set; }

        // Contact Info
        [Required, EmailAddress]
        public string Email { get; set; }

        [EmailAddress]
        public string AlternateEmail { get; set; }

        [Required, MaxLength(15)]
        public string PrimaryMobile { get; set; }

        [MaxLength(15)]
        public string SecondaryMobile { get; set; }

        // Emergency Contact
        [Required, MaxLength(100)]
        public string EmergencyContactName { get; set; }

        [Required, MaxLength(50)]
        public string EmergencyContactRelation { get; set; }

        [Required, MaxLength(15)]
        public string EmergencyContactNumber { get; set; }

        // Other Info
        [Required, MaxLength(20)]
        public string Gender { get; set; }

        [MaxLength(10)]
        public string BloodGroup { get; set; }

        [MaxLength(10)]
        public string MaritalStatus { get; set; }

        [MaxLength(50)]
        public string Religion { get; set; }

        [Required, MaxLength(50)]
        public string Ethnicity { get; set; }

        public bool HasDisability { get; set; } = false;

        [MaxLength(100)]
        public string DisabilityType { get; set; }

        public int? DisabilityPercentage { get; set; }
    }
}
