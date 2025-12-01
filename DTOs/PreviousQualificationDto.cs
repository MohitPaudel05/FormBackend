using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class PreviousQualificationDto
    {
        public int Id { get; set; }
        public string Qualification { get; set; }
        public string? BoardOrUniversity { get; set; }
        public string? InstitutionName { get; set; }
        public int? PassedYear { get; set; }
        public string? DivisionOrGPA { get; set; }
        public string? MarksheetPath { get; set; }
        public string? ProvisionalCardPath { get; set; }
        public string? PhotoPath { get; set; }
        public string? SignaturePath { get; set; }
    }
    }
