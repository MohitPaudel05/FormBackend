using FormBackend.Enums;

namespace FormBackend.Models
{
    public class AcademicSession
    {
        public int Id { get; set; }
        public int ProgramEnrollmentId { get; set; }
        public ProgramEnrollment ProgramEnrollment { get; set; } = null!;

        public AcademicYear AcademicYear { get; set; }
        public Semester Semester { get; set; }
        public string Section { get; set; } = string.Empty;
        public string RollNumber { get; set; } = string.Empty;
        public AcademicStatus Status { get; set; }
    }
}
