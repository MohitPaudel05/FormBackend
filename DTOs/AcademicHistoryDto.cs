using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class AcademicHistoryDto
    {
        public string Qualification { get; set; } = string.Empty;  // string from frontend
        public string Board { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public int PassedYear { get; set; }
        public string DivisionGPA { get; set; } = string.Empty;    // string from frontend

        public IFormFile? Marksheet { get; set; }
        public IFormFile? Provisional { get; set; }

        public string? MarksheetPath { get; set; }
        public string? ProvisionalPath { get; set; }

        public int StudentId { get; set; }
    }
}
