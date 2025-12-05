using FormBackend.Enums;

namespace FormBackend.Models
{
    public class Emergency
    {
        public int Id { get; set; }
        public string EmergencyContactName { get; set; } = string.Empty;
        public RelationType EmergencyContactRelation { get; set; }
        public string EmergencyContactNumber { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
