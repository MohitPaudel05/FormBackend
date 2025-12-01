using FormBackend.Enums;

namespace FormBackend.Models
{
    public class Ethnicity
    {
        public int Id { get; set; }
        public EthnicityName EthnicityName { get; set; } 
        public EthnicityGroup  EthnicityGroup { get; set; } 
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
