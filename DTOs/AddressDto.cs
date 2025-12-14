namespace FormBackend.DTOs
{
    public class AddressDto
    {
        public string AddressType { get; set; } = string.Empty; // "Permanent" / "Temporary" / "SameAsPermanent"
        public string Province { get; set; } = string.Empty; // enum as string
        public string District { get; set; } = string.Empty;
        public string Municipality { get; set; } = string.Empty;
        public string WardNumber { get; set; } = string.Empty;
        public string? Tole { get; set; }
        public string? HouseNumber { get; set; }
    }
}
