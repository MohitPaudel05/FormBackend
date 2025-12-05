using AutoMapper;
using FormBackend.DTOs;
using FormBackend.Enums;
using FormBackend.Helpers;
using FormBackend.Models;
using FormBackend.Unit_Of_Work;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;
using System.Net;
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
            //citizenship

            if (dto.Citizenship != null)
            {
                // Save citizenship front photo if provided
                if (dto.Citizenship.CitizenshipFrontPhoto != null)
                {
                    dto.Citizenship.CitizenshipFrontPhotoPath = await FileHelper.SaveImageAsync(
                        dto.Citizenship.CitizenshipFrontPhoto,
                        _wwwrootPath,
                        "citizenship"
                    );
                }

                // Save citizenship back photo if provided
                if (dto.Citizenship.CitizenshipBackPhoto != null)
                {
                    dto.Citizenship.CitizenshipBackPhotoPath = await FileHelper.SaveImageAsync(
                        dto.Citizenship.CitizenshipBackPhoto,
                        _wwwrootPath,
                        "citizenship"
                    );
                }

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

            // Addresses - Now iterates through List<AddressDto>
            if (dto.Addresses != null && dto.Addresses.Count > 0)
            {
                foreach (var addressDto in dto.Addresses)
                {
                    // Skip empty addresses (e.g., when SameAsPermanent is selected, we may not need temporary)
                    if (string.IsNullOrEmpty(addressDto.District))
                        continue;

                    var address = new Address
                    {
                        StudentId = studentId,
                        District = addressDto.District,
                        Municipality = addressDto.Municipality,
                        Province = Enum.Parse<Province>(addressDto.Province),
                        WardNumber = addressDto.WardNumber,
                        Tole = addressDto.Tole,
                        HouseNumber = addressDto.HouseNumber
                    };

                    // Determine AddressType
                    address.AddressType = Enum.Parse<AddressType>(addressDto.AddressType);

                    await _unitOfWork.Addresses.AddAsync(address);
                }
                await _unitOfWork.CompleteAsync();
            }

            // Parents 
            if (dto.Parents != null && dto.Parents.Count > 0)
            {
                foreach (var parentDto in dto.Parents)
                {
                    if (string.IsNullOrEmpty(parentDto.FullName))
                        continue;

                    var parent = _mapper.Map<ParentDetail>(parentDto);
                    parent.StudentId = studentId;
                    await _unitOfWork.ParentDetails.AddAsync(parent);
                }
                await _unitOfWork.CompleteAsync();
            }

            // Scholarships
            if (dto.Scholarships != null)
            {
                var scholarship = _mapper.Map<Scholarship>(dto.Scholarships);
                scholarship.StudentId = studentId;
                await _unitOfWork.Scholarships.AddAsync(scholarship);
            }

            // Bank Details

            if (dto.BankDetails != null)
            {
                var bank = _mapper.Map<BankDetail>(dto.BankDetails);
                bank.StudentId = studentId;
                await _unitOfWork.BankDetails.AddAsync(bank);
            }

            // Student Extra Info
            if (dto.StudentExtraInfos != null)
            {
                var extraInfo = _mapper.Map<StudentExtraInfo>(dto.StudentExtraInfos);
                extraInfo.StudentId = studentId;
                await _unitOfWork.StudentExtraInfos.AddAsync(extraInfo);
            }

            // Achievements
            if (dto.Achievements != null && dto.Achievements.Any())
            {
                foreach (var achDto in dto.Achievements)
                {
                    var achievement = _mapper.Map<Achievement>(achDto);
                    achievement.StudentId = studentId;
                    await _unitOfWork.Achievements.AddAsync(achievement);
                }
            }

            // Academic Histories
            if (dto.AcademicHistories != null && dto.AcademicHistories.Any())
            {
                foreach (var acDto in dto.AcademicHistories)
                {
                    // Only skip completely empty entries
                    if (string.IsNullOrEmpty(acDto.Qualification) && string.IsNullOrEmpty(acDto.Board))
                        continue;

                    // Save uploaded files if they exist
                    if (acDto.Marksheet != null)
                        acDto.MarksheetPath = await FileHelper.SaveDocumentAsync(acDto.Marksheet, _wwwrootPath, "documents");

                    if (acDto.Provisional != null)
                        acDto.ProvisionalPath = await FileHelper.SaveDocumentAsync(acDto.Provisional, _wwwrootPath, "documents");

                    // NEW: Save photo, signature, and character certificate
                    if (acDto.Photo != null)
                        acDto.PhotoPath = await FileHelper.SaveImageAsync(acDto.Photo, _wwwrootPath, "academic");

                    if (acDto.Signature != null)
                        acDto.SignaturePath = await FileHelper.SaveSignatureAsync(acDto.Signature, _wwwrootPath, "academic");

                    if (acDto.CharacterCertificate != null)
                        acDto.CharacterCertificatePath = await FileHelper.SaveDocumentAsync(acDto.CharacterCertificate, _wwwrootPath, "documents");

                    // Map to entity and assign StudentId
                    var academic = _mapper.Map<AcademicHistory>(acDto);
                    academic.StudentId = studentId;

                    await _unitOfWork.AcademicHistories.AddAsync(academic);
                }
            }
            // Program Enrollment & Academic Sessions
            if (dto.ProgramEnrollments != null)
            {
                var enrollment = _mapper.Map<ProgramEnrollment>(dto.ProgramEnrollments);
                enrollment.StudentId = studentId;
                enrollment.EnrollmentDate = DateTime.Now;

                await _unitOfWork.ProgramEnrollments.AddAsync(enrollment);
                await _unitOfWork.CompleteAsync();  // SAVE FIRST to get the ID

                // Now enrollment.Id has the correct value
                if (dto.ProgramEnrollments.AcademicSessions != null && dto.ProgramEnrollments.AcademicSessions.Any())
                {
                    foreach (var sessionDto in dto.ProgramEnrollments.AcademicSessions)
                    {
                        var session = _mapper.Map<AcademicSession>(sessionDto);
                        session.ProgramEnrollmentId = enrollment.Id;  // Now this has the correct ID
                        await _unitOfWork.AcademicSessions.AddAsync(session);
                    }
                }
            }

            // Declaration
            if (dto.Declaration != null)
            {
                var declaration = _mapper.Map<Declaration>(dto.Declaration);
                declaration.StudentId = studentId;
                declaration.DateOfApplication = DateOnly.FromDateTime(DateTime.Now);
                await _unitOfWork.Declarations.AddAsync(declaration);
            }

            // SAVE ALL CHANGES
            await _unitOfWork.CompleteAsync();

            // 4. Return full DTO
            return await GetStudentByIdAsync(studentId)!;
        }


        // Get full student by ID
        public async Task<StudentFullDto> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return null;

            var secondary = (await _unitOfWork.SecondaryInfos.FindAsync(s => s.StudentId == id)).FirstOrDefault();
            var disability = (await _unitOfWork.Disabilities.FindAsync(d => d.StudentId == id)).FirstOrDefault();
            var citizenship = (await _unitOfWork.CitizenShips.FindAsync(c => c.StudentId == id)).FirstOrDefault();
            var ethnicity = (await _unitOfWork.Ethnicities.FindAsync(e => e.StudentId == id)).FirstOrDefault();
            var emergency = (await _unitOfWork.Emergencies.FindAsync(e => e.StudentId == id)).FirstOrDefault();
            var academicHistories = await _unitOfWork.AcademicHistories.FindAsync(a => a.StudentId == id);
            var addresses = await _unitOfWork.Addresses.FindAsync(a => a.StudentId == id);
            var parents = await _unitOfWork.ParentDetails.FindAsync(p => p.StudentId == id);  // Get all parents
            var scholarship = (await _unitOfWork.Scholarships.FindAsync(s => s.StudentId == id)).FirstOrDefault();
            var bank = (await _unitOfWork.BankDetails.FindAsync(b => b.StudentId == id)).FirstOrDefault();
            var extraInfo = (await _unitOfWork.StudentExtraInfos.FindAsync(e => e.StudentId == id)).FirstOrDefault();
            var achievements = await _unitOfWork.Achievements.FindAsync(a => a.StudentId == id);
            var enrollment = (await _unitOfWork.ProgramEnrollments.FindAsync(e => e.StudentId == id)).FirstOrDefault();
            var declaration = (await _unitOfWork.Declarations.FindAsync(d => d.StudentId == id)).FirstOrDefault();

            return new StudentFullDto
            {
                Student = _mapper.Map<StudentDto>(student),
                SecondaryInfo = _mapper.Map<SecondaryInfoDto>(secondary),
                Disability = _mapper.Map<DisabilityDto>(disability),
                Citizenship = _mapper.Map<CitizenShipDto>(citizenship),
                Ethnicity = _mapper.Map<EthnicityDto>(ethnicity),
                Emergency = _mapper.Map<EmergencyDto>(emergency),
                Addresses = _mapper.Map<List<AddressDto>>(addresses),
                Parents = _mapper.Map<List<ParentDetailDto>>(parents),  // Map to List
                Scholarships = _mapper.Map<ScholarshipDto>(scholarship),
                BankDetails = _mapper.Map<BankDetailDto>(bank),
                StudentExtraInfos = _mapper.Map<StudentExtraInfoDto>(extraInfo),
                Achievements = _mapper.Map<List<AchievementDto>>(achievements),
                AcademicHistories = _mapper.Map<List<AcademicHistoryDto>>(academicHistories),
                ProgramEnrollments = _mapper.Map<ProgramEnrollmentDto>(enrollment),
                Declaration = _mapper.Map<DeclarationDto>(declaration)
            };
        }
    }
}