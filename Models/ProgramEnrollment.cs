using FormBackend.Enums;

namespace FormBackend.Models
{
    public class ProgramEnrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Faculty Faculty { get; set; }
        public DegreeProgram DegreeProgram { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;

        public ICollection<AcademicSession> AcademicSessions { get; set; } = new List<AcademicSession>();
    }
}
