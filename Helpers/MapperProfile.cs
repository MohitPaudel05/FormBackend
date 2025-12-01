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
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)));

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
            CreateMap<Emergency, EmergencyDto>().ReverseMap();

            // Disability
            CreateMap<Disability, DisabilityDto>()
                .ForMember(dest => dest.DisabilityStatus, opt => opt.MapFrom(src => src.DisabilityStatus.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.DisabilityStatus, opt => opt.MapFrom(src => Enum.Parse<DisabilityStatus>(src.DisabilityStatus)));

            // CitizenShip
            CreateMap<CitizenShip, CitizenShipDto>().ReverseMap();


            // Address

            CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.AddressType, opt => opt.MapFrom(src => src.AddressType.ToString()))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province.ToString()))
           .ReverseMap()
            .ForMember(dest => dest.AddressType, opt => opt.MapFrom(src => Enum.Parse<AddressType>(src.AddressType)))
          .ForMember(dest => dest.Province, opt => opt.MapFrom(src => Enum.Parse<Province>(src.Province)));
            // Parents details 
           
            CreateMap<ParentDetail, ParentDetailDto>()
                .ForMember(dest => dest.ParentType, opt => opt.MapFrom(src => src.ParentType.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.ParentType, opt => opt.MapFrom(src => Enum.Parse<ParentType>(src.ParentType)));

            //Academic history

            CreateMap<AcademicHistory, AcademicHistoryDto>()
     .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification.ToString()))
     .ForMember(dest => dest.DivisionGPA, opt => opt.MapFrom(src => src.DivisionGPA.ToString()))
     .ReverseMap()
     .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => Enum.Parse<QualificationType>(src.Qualification)))
     .ForMember(dest => dest.DivisionGPA, opt => opt.MapFrom(src => Enum.Parse<DivisionGPA>(src.DivisionGPA)));

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
            CreateMap<Declaration, DeclarationDto>().ReverseMap();

            // ProgramEnrollment ↔ ProgramEnrollmentDto
            CreateMap<ProgramEnrollment, ProgramEnrollmentDto>()
                .ForMember(dest => dest.AcademicSessions, opt => opt.MapFrom(src => src.AcademicSessions))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore());


            // AcademicSession ↔ AcademicSessionDto
            CreateMap<AcademicSession, AcademicSessionDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProgramEnrollment, opt => opt.Ignore());



            // Full DTO mapping (nested objects)
            CreateMap<StudentFullDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
