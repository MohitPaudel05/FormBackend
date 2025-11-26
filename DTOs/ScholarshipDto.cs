using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace FormBackend.DTOs
{
    public class ScholarshipDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string ScholarshipType { get; set; }

        [MaxLength(100)]
        public string ProviderName { get; set; }
        [Precision(18, 2)] 
        public decimal? Amount { get; set; }
    }
}
