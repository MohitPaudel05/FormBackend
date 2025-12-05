using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string ImagePath { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public Gender Gender { get; set; }



        public ProgramEnrollment? ProgramEnrollment { get; set; }
    }
}
