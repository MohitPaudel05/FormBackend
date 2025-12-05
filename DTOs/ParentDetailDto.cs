namespace FormBackend.DTOs
{
    public class ParentDetailDto
    {
        public string ParentType { get; set; } = string.Empty; // "Father", "Mother", "Guardian"
        public string FullName { get; set; } = string.Empty;
        public string? Occupation { get; set; } = string.Empty;
        public string? Designation { get; set; } = string.Empty;
        public string? Organization { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        
        public string? AnnualFamilyIncome { get; set; } = string.Empty; // enum as string
    }
}
