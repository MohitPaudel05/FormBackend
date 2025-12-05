using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class ProgramEnrollmentDto
    {
        public Faculty Faculty { get; set; }
        public DegreeProgram DegreeProgram { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;

        public List<AcademicSessionDto> AcademicSessions { get; set; } = new();
    }
}
