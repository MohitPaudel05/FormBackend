using FormBackend.Enums;

namespace FormBackend.DTOs
{
    public class AddressDto
    {
        public int  Id { get; set; }

        public AddressType AddressType { get; set; }

        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public string WardNumber { get; set; }
        public string Tole { get; set; }
        public string HouseNumber { get; set; }
    }
}
