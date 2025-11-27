using FormBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBackend.Models
{
    public class EmergencyContact
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        [Required]
        public string EmergencyContactName { get; set; }
        [Required]
        public  ParentType EmergencyContactRelation { get; set; }
        [Required, MaxLength(15)]
        public string EmergencyContactNumber{ get; set; }


    }
}
