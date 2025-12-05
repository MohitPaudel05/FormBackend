namespace FormBackend.DTOs
{
    public class StudentFullDto
    {
        public StudentDto Student { get; set; } = new();
        public SecondaryInfoDto SecondaryInfo { get; set; } = new();
        public EthnicityDto Ethnicity { get; set; } = new();
        public EmergencyDto Emergency { get; set; } = new();
        public DisabilityDto Disability { get; set; } = new();
        public CitizenShipDto Citizenship { get; set; } = new();

        //address
        public List<AddressDto> Addresses { get; set; } = new();

        public List<ParentDetailDto> Parents { get; set; } = new();

        public  ProgramEnrollmentDto ProgramEnrollments { get; set; } = new();

        public List<AcademicHistoryDto> AcademicHistories { get; set; } = new();

        public ScholarshipDto Scholarships { get; set; }

        public BankDetailDto BankDetails { get; set; }

        public List<AchievementDto> Achievements { get; set; } = new();
        public StudentExtraInfoDto StudentExtraInfos { get; set; }

        public DeclarationDto Declaration { get; set; }
    }
}
