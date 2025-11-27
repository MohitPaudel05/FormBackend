using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Faculty { get; set; } 

        [Required, MaxLength(100)]
        public string Program { get; set; }

        [Required, MaxLength(50)]
        public string CourseLevel { get; set; }

        [Required, MaxLength(20)]
        public string AcademicYear { get; set; }

        [MaxLength(50)]
        public string SemesterOrClass { get; set; }

        [MaxLength(5)]
        public string Section { get; set; }

        [MaxLength(50)]
        public string RollNumber { get; set; }

        [MaxLength(50)]
        public string RegistrationNumber { get; set; }

        public DateTime? EnrollDate { get; set; }

        [MaxLength(20)]
        public string AcademicStatus { get; set; }
    }
}
