using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.DTOs
{
    public class EmergencyContactDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string EmergencyContactName { get; set; }

        [Required]
        public ParentType EmergencyContactRelation { get; set; }

        [Required, MaxLength(15)]
        public string EmergencyContactNumber { get; set; }

        [EmailAddress, MaxLength(100)]
        public string EmergencyContactEmail { get; set; }
    }
}
