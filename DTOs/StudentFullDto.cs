namespace FormBackend.DTOs
{
    public class StudentFullDto
    {
        public PersonalDetailDto PersonalDetail { get; set; }
        
        public List<ParentDetailDto> Parents { get; set; }
        public EnrollmentDto Enrollment { get; set; }
        public List<QualificationDto> Qualifications { get; set; }
        public List<StudentDocumentDto> Documents { get; set; }
        public FeeDetailDto FeeDetail { get; set; }
        public ScholarshipDto Scholarship { get; set; }
        public BankDetailDto BankDetail { get; set; }
        public List<StudentInterestDto> Interests { get; set; }
        public List<AwardDto> Awards { get; set; }
        public HostelTransportDetailDto HostelTransport { get; set; }
        public DeclarationDto Declaration { get; set; }

        public DisabilityDto Disability { get; set; }

        public CitizenshipInfoDto CitizenshipInfo { get; set; }

        public ContactInfoDto ContactInfo { get; set; }

        public ReligionDto Religion { get; set; }
        public List<AddressDto> Address { get; set; }

        public List<EmergencyContactDto> EmergencyContacts { get; set; }
    }
}
