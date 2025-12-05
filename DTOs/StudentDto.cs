namespace FormBackend.DTOs
{
    public class StudentDto
    {
        public IFormFile? Image { get; set; }   // For upload
        public string? ImagePath { get; set; }  // For display

        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;
    }
}
