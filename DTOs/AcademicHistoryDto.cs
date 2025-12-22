using FormBackend.Enums;
using System.Text.Json.Serialization;

namespace FormBackend.DTOs
{
    public class AcademicHistoryDto
    {
        public int ?Id { get; set; }
        public string Qualification { get; set; } 
        public string Board { get; set; }
        public string Institution { get; set; }
        public string PassedYear { get; set; }     
        public string DivisionGPA { get; set; }   
        [JsonIgnore]
        public IFormFile? Marksheet { get; set; }
        [JsonIgnore]
        public IFormFile? Provisional { get; set; }
        [JsonIgnore]
        public IFormFile? Photo { get; set; }
        [JsonIgnore]
        public IFormFile? Signature { get; set; }
        [JsonIgnore]
        public IFormFile? CharacterCertificate { get; set; }

        public string? MarksheetPath { get; set; }
        public string? ProvisionalPath { get; set; }
        public string? PhotoPath { get; set; }
        public string? SignaturePath { get; set; }
        public string? CharacterCertificatePath { get; set; }

    }
}
