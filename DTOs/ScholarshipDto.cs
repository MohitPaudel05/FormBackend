using System.ComponentModel.DataAnnotations.Schema;

namespace FormBackend.DTOs
{
    public class ScholarshipDto
    {
        public string FeeCategory { get; set; } = string.Empty; // "Regular", "SelfFinanced", "Scholarship", "Quota"
        public string? ScholarshipType { get; set; }
        public string? ScholarshipProvider { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ScholarshipAmount { get; set; }
    }
}