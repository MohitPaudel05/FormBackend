using AutoMapper;
using FormBackend.DTOs;
using FormBackend.Enums;
using FormBackend.Helpers;
using FormBackend.Models;
using FormBackend.Unit_Of_Work;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace FormBackend.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _wwwrootPath;
        private readonly string _imageFolder = "images";

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wwwrootPath = env.WebRootPath;
        }

        // Add full student info
        public async Task<StudentFullDto> AddStudentAsync(StudentFullDto dto)
        {
            // 1. Save student image if provided
            if (dto.Student.Image != null)
            {
                dto.Student.ImagePath = await FileHelper.SaveImageAsync(
                    dto.Student.Image,
                    _wwwrootPath,
                    _imageFolder
                );
            }

            // 2. Save Student table
            var student = _mapper.Map<Student>(dto.Student);
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync(); // Save to get Student Id
            int studentId = student.Id;

            // 3. Save related tables
            if (dto.SecondaryInfo != null)
            {
                var secondary = _mapper.Map<SecondaryInfo>(dto.SecondaryInfo);
                secondary.StudentId = studentId;
                await _unitOfWork.SecondaryInfos.AddAsync(secondary);
            }

            if (dto.Disability != null)
            {
                var disability = _mapper.Map<Disability>(dto.Disability);
                disability.StudentId = studentId;
                await _unitOfWork.Disabilities.AddAsync(disability);
            }

            if (dto.Citizenship != null)
            {
                var citizenship = _mapper.Map<CitizenShip>(dto.Citizenship);
                citizenship.StudentId = studentId;
                await _unitOfWork.CitizenShips.AddAsync(citizenship);
            }

            if (dto.Ethnicity != null)
            {
                var ethnicity = _mapper.Map<Ethnicity>(dto.Ethnicity);
                ethnicity.StudentId = studentId;
                await _unitOfWork.Ethnicities.AddAsync(ethnicity);
            }

            if (dto.Emergency != null)
            {
                var emergency = _mapper.Map<Emergency>(dto.Emergency);
                emergency.StudentId = studentId;
                await _unitOfWork.Emergencies.AddAsync(emergency);
            }
            // address
            if (dto.Addresses != null && dto.Addresses.Any())
            {
                foreach (var addrDto in dto.Addresses)
                {
                    // Parse AddressType from string to enum
                    if (!Enum.TryParse<AddressType>(addrDto.AddressType, out var addrType))
                    {
                        addrType = AddressType.Permanent; // default
                    }

                    var address = new Address
                    {
                        AddressType = addrType,
                        Province = Enum.Parse<Province>(addrDto.Province),
                        District = addrDto.District,
                        Municipality = addrDto.Municipality,
                        WardNumber = addrDto.WardNumber,
                        Tole = addrDto.Tole,
                        HouseNumber = addrDto.HouseNumber,
                        StudentId = studentId
                    };

                    await _unitOfWork.Addresses.AddAsync(address);

                    // If "SameAsPermanent", copy Permanent info as a new row
                    if (addrType == AddressType.SameAsPermanent)
                    {
                        var permanent = dto.Addresses.FirstOrDefault(a => a.AddressType == "Permanent");
                        if (permanent != null)
                        {
                            var copyAddress = new Address
                            {
                                AddressType = AddressType.SameAsPermanent,
                                Province = Enum.Parse<Province>(permanent.Province),
                                District = permanent.District,
                                Municipality = permanent.Municipality,
                                WardNumber = permanent.WardNumber,
                                Tole = permanent.Tole,
                                HouseNumber = permanent.HouseNumber,
                                StudentId = studentId
                            };
                            await _unitOfWork.Addresses.AddAsync(copyAddress);
                        }
                    }
                }
            }


            //Parent Details
              if (dto.Parents != null && dto.Parents.Any())
            {
                foreach (var parentDto in dto.Parents)
                {
                    var parent = _mapper.Map<ParentDetail>(parentDto);
                    parent.StudentId = studentId;
                    await _unitOfWork.ParentDetails.AddAsync(parent);
                }
            }

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CompleteAsync();


            

            // 4. Return full DTO
            return await GetStudentByIdAsync(studentId)!;
        }

















        // Get full student by ID
        public async Task<StudentFullDto?> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return null;

            var secondary = (await _unitOfWork.SecondaryInfos.FindAsync(s => s.StudentId == id)).FirstOrDefault();
            var disability = (await _unitOfWork.Disabilities.FindAsync(d => d.StudentId == id)).FirstOrDefault();
            var citizenship = (await _unitOfWork.CitizenShips.FindAsync(c => c.StudentId == id)).FirstOrDefault();
            var ethnicity = (await _unitOfWork.Ethnicities.FindAsync(e => e.StudentId == id)).FirstOrDefault();
            var emergency = (await _unitOfWork.Emergencies.FindAsync(e => e.StudentId == id)).FirstOrDefault();

            var addresses = await _unitOfWork.Addresses.FindAsync(a => a.StudentId == id);
            var parents = await _unitOfWork.ParentDetails.FindAsync(p => p.StudentId == id);

            return new StudentFullDto
            {
                Student = _mapper.Map<StudentDto>(student),
                SecondaryInfo = _mapper.Map<SecondaryInfoDto>(secondary),
                Disability = _mapper.Map<DisabilityDto>(disability),
                Citizenship = _mapper.Map<CitizenShipDto>(citizenship),
                Ethnicity = _mapper.Map<EthnicityDto>(ethnicity),
                Emergency = _mapper.Map<EmergencyDto>(emergency),
                Addresses = _mapper.Map<List<AddressDto>>(addresses),
                Parents = _mapper.Map<List<ParentDetailDto>>(parents)
            };
        }
    }
}
