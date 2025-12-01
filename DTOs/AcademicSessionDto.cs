using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class AcademicSessionDto
    {
        public AcademicYear AcademicYear { get; set; }
        public Semester Semester { get; set; }
        public string Section { get; set; } = string.Empty;
        public string RollNumber { get; set; } = string.Empty;
        public AcademicStatus Status { get; set; }
    }
}
