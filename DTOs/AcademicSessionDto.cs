using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class AcademicSessionDto
    {
        public string AcademicYear { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string RollNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
