using AutoMapper;
using FormBackend.DTOs;
using FormBackend.Enums;
using FormBackend.Models;

namespace FormBackend.Helpers
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentDto>()
                // Map Gender enum to string
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                // Map DateOnly -> DateTime
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
                .ReverseMap()
                // Map Gender string -> enum
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)))
                // Map DateTime -> DateOnly
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // SecondaryInfo
            CreateMap<SecondaryInfo, SecondaryInfoDto>()
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup.ToString()))
                .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus.ToString()))
                .ForMember(dest => dest.Religion, opt => opt.MapFrom(src => src.Religion.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => Enum.Parse<BloodGroup>(src.BloodGroup)))
                .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => Enum.Parse<MaritalStatus>(src.MaritalStatus)))
                .ForMember(dest => dest.Religion, opt => opt.MapFrom(src => Enum.Parse<Religion>(src.Religion)));

            // Ethnicity
            CreateMap<Ethnicity, EthnicityDto>()
                .ForMember(dest => dest.EthnicityGroup, opt => opt.MapFrom(src => src.EthnicityGroup.ToString()))
                .ForMember(dest => dest.EthnicityName, opt => opt.MapFrom(src => src.EthnicityName.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.EthnicityGroup, opt => opt.MapFrom(src => Enum.Parse<EthnicityGroup>(src.EthnicityGroup)))
                .ForMember(dest => dest.EthnicityName, opt => opt.MapFrom(src => Enum.Parse<EthnicityName>(src.EthnicityName)));

            // Emergency
            CreateMap<Emergency, EmergencyDto>()
         .ForMember(dest => dest.EmergencyContactRelation,
             opt => opt.MapFrom(src => src.EmergencyContactRelation.ToString()))
         .ReverseMap()
         .ForMember(dest => dest.EmergencyContactRelation,
             opt => opt.MapFrom(src => Enum.Parse<RelationType>(src.EmergencyContactRelation, true)));

            // Disability
            CreateMap<Disability, DisabilityDto>()
                .ForMember(dest => dest.DisabilityStatus,
                    opt => opt.MapFrom(src => src.DisabilityStatus.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.DisabilityStatus,
                    opt => opt.MapFrom(src => string.IsNullOrEmpty(src.DisabilityStatus)
                        ? DisabilityStatus.None
                        : Enum.Parse<DisabilityStatus>(src.DisabilityStatus, true)));

            // CitizenShip
            CreateMap<CitizenShipDto, CitizenShip>()
                .ForMember(dest => dest.CitizenshipFrontPhotoPath, opt => opt.MapFrom(src => src.CitizenshipFrontPhotoPath))
                .ForMember(dest => dest.CitizenshipBackPhotoPath, opt => opt.MapFrom(src => src.CitizenshipBackPhotoPath))
                .ReverseMap()
                .ForMember(dest => dest.CitizenshipFrontPhoto, opt => opt.Ignore())
                .ForMember(dest => dest.CitizenshipBackPhoto, opt => opt.Ignore());




            // ===== ADDRESS =====
            // AddressDto -> Address
            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Province, opt =>
                    opt.MapFrom(src => Enum.Parse<Province>(src.Province, true)))
                .ForMember(dest => dest.AddressType, opt =>
                    opt.MapFrom(src =>
                        src.AddressType == "SameAsPermanent"
                            ? AddressType.Permanent   // Force Permanent
                            : Enum.Parse<AddressType>(src.AddressType, true)
                    ));

            // Address -> AddressDto
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.AddressType, opt =>
                    opt.MapFrom(src => src.AddressType.ToString()))
                .ForMember(dest => dest.Province, opt =>
                    opt.MapFrom(src => src.Province.ToString()));
            // ===== PARENT DETAIL =====
            CreateMap<ParentDetailDto, ParentDetail>()
                .ForMember(dest => dest.ParentType, opt => opt.MapFrom(src => Enum.Parse<ParentType>(src.ParentType)))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation ?? string.Empty))
                .ForMember(dest => dest.Designation, opt => opt.MapFrom(src => src.Designation ?? string.Empty))
                .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.Organization ?? string.Empty))
                .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email ?? string.Empty))
                .ForMember(dest => dest.FamilyIncome, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.AnnualFamilyIncome) ? (AnnualIncome?)null : Enum.Parse<AnnualIncome>(src.AnnualFamilyIncome)))
                .ReverseMap()
                .ForMember(dest => dest.ParentType, opt => opt.MapFrom(src => src.ParentType.ToString()))
                .ForMember(dest => dest.AnnualFamilyIncome, opt => opt.MapFrom(src => src.FamilyIncome.HasValue ? src.FamilyIncome.ToString() : string.Empty));

            // ---------------- AcademicHistory ----------------
            CreateMap<AcademicHistoryDto, AcademicHistory>()
    .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => Enum.Parse<QualificationType>(src.Qualification, true)))
    .ForMember(dest => dest.DivisionGPA, opt => opt.MapFrom(src => Enum.Parse<DivisionGPA>(src.DivisionGPA, true)))
    .ForMember(dest => dest.MarksheetPath, opt => opt.MapFrom(src => src.MarksheetPath))
    .ForMember(dest => dest.ProvisionalPath, opt => opt.MapFrom(src => src.ProvisionalPath))
    .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.PhotoPath))
    .ForMember(dest => dest.SignaturePath, opt => opt.MapFrom(src => src.SignaturePath))
    .ForMember(dest => dest.CharacterCertificatePath, opt => opt.MapFrom(src => src.CharacterCertificatePath))
    .ReverseMap()
    .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification.ToString()))
    .ForMember(dest => dest.DivisionGPA, opt => opt.MapFrom(src => src.DivisionGPA.ToString()));


            //scholarship 
            CreateMap<Scholarship, ScholarshipDto>()
           .ForMember(dest => dest.FeeCategory, opt => opt.MapFrom(src => src.FeeCategory.ToString()))
          .ForMember(dest => dest.ScholarshipType, opt => opt.MapFrom(src => src.ScholarshipType.ToString()))
          .ReverseMap()
          .ForMember(dest => dest.FeeCategory, opt => opt.MapFrom(src => Enum.Parse<FeeCategory>(src.FeeCategory)))
          .ForMember(dest => dest.ScholarshipType, opt => opt.MapFrom(src =>
         string.IsNullOrEmpty(src.ScholarshipType) ? ScholarshipType.None : Enum.Parse<ScholarshipType>(src.ScholarshipType)));
            //BANK DETAILS

            CreateMap<BankDetail, BankDetailDto>().ReverseMap();
            // ACHIEVEMENTS
            CreateMap<Achievement, AchievementDto>().ReverseMap();

            // StudentExtraInfo Mapping (strict — no null checks)
            CreateMap<StudentExtraInfo, StudentExtraInfoDto>()
          .ForMember(dest => dest.ExtracurricularInterests,
          opt => opt.MapFrom(src => src.ExtracurricularInterests.Split(new[] { ',' })))
         .ReverseMap()
         .ForMember(dest => dest.ExtracurricularInterests,
          opt => opt.MapFrom(src => string.Join(",", src.ExtracurricularInterests)));

            //declaration mapping
            CreateMap<DeclarationDto, Declaration>()
    .ForMember(dest => dest.IsAgreed, opt => opt.MapFrom(src => src.IsAgreed))
    .ReverseMap()
    .ForMember(dest => dest.IsAgreed, opt => opt.MapFrom(src => src.IsAgreed));


            // Program Enrollment
            CreateMap<ProgramEnrollment, ProgramEnrollmentDto>()
      .ForMember(dest => dest.Faculty, opt => opt.MapFrom(src => src.Faculty.ToString()))
      .ForMember(dest => dest.DegreeProgram, opt => opt.MapFrom(src => src.DegreeProgram.ToString()))
      .ForMember(dest => dest.AcademicSessions, opt => opt.MapFrom(src => src.AcademicSessions))
      .ReverseMap()
      .ForMember(dest => dest.Faculty, opt => opt.MapFrom(src =>
          string.IsNullOrEmpty(src.Faculty)
              ? Faculty.Science
              : Enum.Parse<Faculty>(src.Faculty.Replace(" ", ""), true)))
      .ForMember(dest => dest.DegreeProgram, opt => opt.MapFrom(src =>
          string.IsNullOrEmpty(src.DegreeProgram)
              ? DegreeProgram.BSc
              : Enum.Parse<DegreeProgram>(src.DegreeProgram.Replace(" ", ""), true)))
      .ForMember(dest => dest.AcademicSessions, opt => opt.MapFrom(src => src.AcademicSessions));

            // Academic Session
            CreateMap<AcademicSession, AcademicSessionDto>()
    .ForMember(dest => dest.AcademicYear, opt => opt.MapFrom(src => src.AcademicYear.ToString()))
    .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester.ToString()))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
    .ReverseMap()
    .ForMember(dest => dest.AcademicYear, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.AcademicYear)
            ? AcademicYear.FirstYear
            : Enum.Parse<AcademicYear>(src.AcademicYear.Replace(" ", ""), true)))
    .ForMember(dest => dest.Semester, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.Semester)
            ? Semester.FirstSemester
            : Enum.Parse<Semester>(src.Semester.Replace(" ", ""), true)))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.Status)
            ? AcademicStatus.Active
            : Enum.Parse<AcademicStatus>(src.Status.Replace(" ", ""), true)));





            // Full DTO mapping (nested objects)
            CreateMap<StudentFullDto, Student>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()) // ignore PK
                 .ForMember(dest => dest.ProgramEnrollment, opt => opt.MapFrom(src => src.ProgramEnrollments));
        }
    }
}
