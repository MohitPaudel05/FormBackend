using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class AcademicHistoryDto
    {
        public string Qualification { get; set; } 
        public string Board { get; set; }
        public string Institution { get; set; }
        public string PassedYear { get; set; }     
        public string DivisionGPA { get; set; }   

        public IFormFile? Marksheet { get; set; }
        public IFormFile? Provisional { get; set; }
        public IFormFile? Photo { get; set; }           
        public IFormFile? Signature { get; set; }      
        public IFormFile? CharacterCertificate { get; set; }  

        public string MarksheetPath { get; set; } = string.Empty;
        public string ProvisionalPath { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public string SignaturePath { get; set; } = string.Empty;
        public string CharacterCertificatePath { get; set; } = string.Empty;

    }
}
