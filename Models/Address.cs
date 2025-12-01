using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public AddressType AddressType { get; set; }  // Use enum

        [Required]
        public Province Province { get; set; }

        [Required]
        public string District { get; set; } = string.Empty;

        [Required]
        public string Municipality { get; set; } = string.Empty;

        [Required]
        public string WardNumber { get; set; } = string.Empty;

        public string? Tole { get; set; }
        public string? HouseNumber { get; set; }

        // Link with Student
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
