using AutoMapper;
using FormBackend.DTOs;
using FormBackend.Models;
using System.Runtime;

namespace FormBackend.Mapping
{
   public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonalDetailDto, PersonalDetail>().ReverseMap();

            CreateMap<ParentDetailDto, ParentDetail>()
            .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relation))
            .ReverseMap();
            CreateMap<EnrollmentDto, Enrollment>().ReverseMap();
            CreateMap<QualificationDto, Qualification>().ReverseMap();
            CreateMap<StudentDocumentDto, StudentDocument>().ReverseMap();
            CreateMap<FeeDetailDto, FeeDetail>().ReverseMap();
            CreateMap<ScholarshipDto, Scholarship>().ReverseMap();
            CreateMap<BankDetailDto, BankDetail>().ReverseMap();
            CreateMap<StudentDocumentDto, StudentDocument>()
               .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
               .ReverseMap();
            CreateMap<AwardDto, Award>().ReverseMap();
            CreateMap<HostelTransportDetailDto, HostelTransportDetail>().ReverseMap();
            CreateMap<DeclarationDto, Declaration>().ReverseMap();
            CreateMap<DisabilityDto, Disability>().ReverseMap();

            CreateMap<CitizenshipInfoDto, CitizenshipInfo>().ReverseMap();
            CreateMap<ContactInfoDto, ContactInfo>().ReverseMap();
          
            CreateMap<ReligionDto, Religion>().ReverseMap();

            CreateMap<AddressDto, Address>()
           .ForMember(dest => dest.AddressType, opt => opt.MapFrom(src => src.AddressType))
           .ReverseMap();

            CreateMap<EmergencyContactDto, EmergencyContact>().ReverseMap();
            CreateMap<StudentInterestDto, StudentInterest>().ReverseMap();
        }
    }
}
