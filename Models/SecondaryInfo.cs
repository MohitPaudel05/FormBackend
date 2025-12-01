using FormBackend.Enums;

namespace FormBackend.Models
{
    public class SecondaryInfo
    {
        public int Id { get; set; }
        
        public string? AlternateEmail { get; set; }
        public string? AlternateMobileNumber { get; set; }
        public BloodGroup BloodGroup { get; set; }    
        public MaritalStatus MaritalStatus { get; set; } 
        public Religion Religion { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
