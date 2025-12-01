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




            // Full DTO mapping (nested objects)
            CreateMap<StudentFullDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
