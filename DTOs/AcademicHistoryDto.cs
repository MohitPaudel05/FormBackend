using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class AcademicHistoryDto
    {
        public string Qualification { get; set; }  // string from frontend
        public string Board { get; set; } 
        public string Institution { get; set; } 
        public int PassedYear { get; set; }
        public string DivisionGPA { get; set; }    // string from frontend

        public IFormFile? Marksheet { get; set; }
        public IFormFile? Provisional { get; set; }

        public string? MarksheetPath { get; set; }
        public string? ProvisionalPath { get; set; }

     
    }
}
