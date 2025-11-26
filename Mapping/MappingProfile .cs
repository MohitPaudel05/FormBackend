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
            CreateMap<PermanentAddressDto, PermanentAddress>().ReverseMap();
            CreateMap<TemporaryAddressDto, TemporaryAddress>().ReverseMap();
            CreateMap<ParentDetailDto, ParentDetail>().ReverseMap();
            CreateMap<EnrollmentDto, Enrollment>().ReverseMap();
            CreateMap<QualificationDto, Qualification>().ReverseMap();
            CreateMap<DocumentDto, Document>().ReverseMap();
            CreateMap<FeeDetailDto, FeeDetail>().ReverseMap();
            CreateMap<ScholarshipDto, Scholarship>().ReverseMap();
            CreateMap<BankDetailDto, BankDetail>().ReverseMap();
            CreateMap<StudentInterestDto, StudentInterest>().ReverseMap();
            CreateMap<AwardDto, Award>().ReverseMap();
            CreateMap<HostelTransportDetailDto, HostelTransportDetail>().ReverseMap();
            CreateMap<DeclarationDto, Declaration>().ReverseMap();
        }
    }
}
