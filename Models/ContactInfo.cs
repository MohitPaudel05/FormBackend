using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class ContactInfo
    {
        public  int Id { get; set; }

        public int StudentId { get; set; }

        public PersonalDetail Student { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [EmailAddress]
        public string AlternateEmail { get; set; }

        [Required, MaxLength(15)]
        public string PrimaryMobile { get; set; }

        [MaxLength(15)]
        public string SecondaryMobile { get; set; }

       
    }
}
