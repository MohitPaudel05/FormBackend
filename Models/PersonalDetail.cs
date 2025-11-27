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

       
        // Contact Info
       

        // Other Info
        [Required, MaxLength(20)]
        public string Gender { get; set; }

        [MaxLength(10)]
        public string BloodGroup { get; set; }

        [MaxLength(10)]
        public string MaritalStatus { get; set; }

        

       

       
    }
}
