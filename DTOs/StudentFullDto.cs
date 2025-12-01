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

        public List<ProgramEnrollmentDto> ProgramEnrollments { get; set; } = new();

        public List<AcademicHistoryDto> AcademicHistories { get; set; } = new();

        public ScholarshipDto Scholarships { get; set; }

        public BankDetailDto BankDetails { get; set; }

        public List<AchievementDto> Achievements { get; set; } = new();
        public StudentExtraInfoDto StudentExtraInfos { get; set; }

        public DeclarationDto Declaration { get; set; }
    }
}
