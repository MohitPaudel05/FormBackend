using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class BankDetail
    {
        public int Id { get; set; }

        [Required]
        public string AccountHolderName { get; set; } = string.Empty;

        [Required]
        public string BankName { get; set; } = string.Empty;

        [Required]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        public string Branch { get; set; } = string.Empty;

        // Link with student
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
