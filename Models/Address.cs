using FormBackend.Enums;

namespace FormBackend.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public PersonalDetail Student { get; set; }

        public AddressType AddressType { get; set; } // 1 for Permanent, 2 for Temporary
        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public string WardNumber { get; set; }
        public string Tole { get; set; }
        public string HouseNumber { get; set; }
    }
}
