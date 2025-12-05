using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class ProgramEnrollmentDto
    {
        public string Faculty { get; set; } = string.Empty;
        public string DegreeProgram { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;

        public List<AcademicSessionDto> AcademicSessions { get; set; } = new();
    }
}
