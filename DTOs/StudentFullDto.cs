namespace FormBackend.DTOs
{
    public class StudentFullDto
    {
        public PersonalDetailDto PersonalDetail { get; set; }
        public PermanentAddressDto PermanentAddress { get; set; }
        public TemporaryAddressDto TemporaryAddress { get; set; } // null if same as permanent
        public List<ParentDetailDto> Parents { get; set; }
        public EnrollmentDto Enrollment { get; set; }
        public List<QualificationDto> Qualifications { get; set; }
        public List<DocumentDto> Documents { get; set; }
        public FeeDetailDto FeeDetail { get; set; }
        public ScholarshipDto Scholarship { get; set; }
        public BankDetailDto BankDetail { get; set; }
        public List<StudentInterestDto> Interests { get; set; }
        public List<AwardDto> Awards { get; set; }
        public HostelTransportDetailDto HostelTransport { get; set; }
        public DeclarationDto Declaration { get; set; }
    }
}
