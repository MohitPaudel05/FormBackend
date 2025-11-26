using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class BankDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required, MaxLength(100)]
        public string AccountHolderName { get; set; }

        [Required, MaxLength(100)]
        public string BankName { get; set; }

        [Required, MaxLength(50)]
        public string AccountNumber { get; set; }

        [MaxLength(100)]
        public string Branch { get; set; }
    }
}
