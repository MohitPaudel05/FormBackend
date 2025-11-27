using FormBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class ContactInfoDto
    {
        public int Id { get; set; }


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
