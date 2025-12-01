namespace FormBackend.DTOs
{
    public class StudentFullDto
    {
        public StudentDto Student { get; set; }
        public SecondaryInfoDto SecondaryInfo { get; set; }
        public EthnicityDto Ethnicity { get; set; }
        public EmergencyDto Emergency { get; set; }
        public DisabilityDto Disability { get; set; }
        public CitizenShipDto Citizenship { get; set; }

        //address
        public List<AddressDto> Addresses { get; set; } = new List<AddressDto>(); 

        public List<ParentDetailDto> Parents { get; set; } = new List<ParentDetailDto>();
    }
}
