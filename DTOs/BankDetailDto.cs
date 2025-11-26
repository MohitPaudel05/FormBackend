using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class BankDetailDto
    {
        public int Id { get; set; }

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
