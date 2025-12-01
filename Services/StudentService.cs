using AutoMapper;
using FormBackend.DTOs;
using FormBackend.Enums;
using FormBackend.Helpers;
using FormBackend.Models;
using FormBackend.Unit_Of_Work;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
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

            // Addresses
            if (dto.Addresses != null && dto.Addresses.Any())
            {
                foreach (var addrDto in dto.Addresses)
                {
                    var address = new Address
                    {
                        AddressType = Enum.TryParse(addrDto.AddressType, out AddressType type) ? type : AddressType.Permanent,
                        Province = Enum.Parse<Province>(addrDto.Province),
                        District = addrDto.District,
                        Municipality = addrDto.Municipality,
                        WardNumber = addrDto.WardNumber,
                        Tole = addrDto.Tole,
                        HouseNumber = addrDto.HouseNumber,
                        StudentId = studentId
                    };
                    await _unitOfWork.Addresses.AddAsync(address);
                }

                await _unitOfWork.CompleteAsync(); // <- add this
            }

            // Parents
            if (dto.Parents != null && dto.Parents.Any())
            {
                foreach (var parentDto in dto.Parents)
                {
                    var parent = _mapper.Map<ParentDetail>(parentDto);
                    parent.StudentId = studentId;
                    await _unitOfWork.ParentDetails.AddAsync(parent);
                }
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
                    if (acDto.Marksheet != null)
                        acDto.MarksheetPath = await FileHelper.SaveDocumentAsync(acDto.Marksheet, _wwwrootPath, "documents");

                    if (acDto.Provisional != null)
                        acDto.ProvisionalPath = await FileHelper.SaveDocumentAsync(acDto.Provisional, _wwwrootPath, "documents");

                    var academic = _mapper.Map<AcademicHistory>(acDto);
                    academic.StudentId = studentId;
                    await _unitOfWork.AcademicHistories.AddAsync(academic);
                }
            }

            // Program Enrollment & Academic Sessions
            if (dto.ProgramEnrollments != null && dto.ProgramEnrollments.Any())
            {
                foreach (var enrollmentDto in dto.ProgramEnrollments)
                {
                    var enrollment = new ProgramEnrollment
                    {
                        StudentId = studentId,
                        Faculty = enrollmentDto.Faculty,
                        DegreeProgram = enrollmentDto.DegreeProgram,
                        EnrollmentDate = enrollmentDto.EnrollmentDate,
                        RegistrationNumber = enrollmentDto.RegistrationNumber
                    };

                    await _unitOfWork.ProgramEnrollments.AddAsync(enrollment);
                    await _unitOfWork.CompleteAsync(); // get enrollment id
                    int enrollmentId = enrollment.Id;

                    if (enrollmentDto.AcademicSessions != null && enrollmentDto.AcademicSessions.Any())
                    {
                        foreach (var sessionDto in enrollmentDto.AcademicSessions)
                        {
                            var session = new AcademicSession
                            {
                                ProgramEnrollmentId = enrollmentId,
                                AcademicYear = sessionDto.AcademicYear,
                                Semester = sessionDto.Semester,
                                Section = sessionDto.Section,
                                RollNumber = sessionDto.RollNumber,
                                Status = sessionDto.Status
                            };
                            await _unitOfWork.AcademicSessions.AddAsync(session);
                        }
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
        public async Task<StudentFullDto?> GetStudentByIdAsync(int id)
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
            var parents = await _unitOfWork.ParentDetails.FindAsync(p => p.StudentId == id);
            var scholarship = (await _unitOfWork.Scholarships.FindAsync(s => s.StudentId == id)).FirstOrDefault();
            var bank = (await _unitOfWork.BankDetails.FindAsync(b => b.StudentId == id)).FirstOrDefault();
            var extraInfo = await _unitOfWork.StudentExtraInfos.FindAsync(e => e.StudentId == id);
            var achievements = await _unitOfWork.Achievements.FindAsync(a => a.StudentId == id);

            // ProgramEnrollments + AcademicSessions
            var enrollments = await _unitOfWork.ProgramEnrollments.FindAsync(e => e.StudentId == id);
            var programEnrollments = new List<ProgramEnrollmentDto>();
            foreach (var enrollment in enrollments)
            {
                var sessions = await _unitOfWork.AcademicSessions.FindAsync(s => s.ProgramEnrollmentId == enrollment.Id);
                var enrollmentDto = _mapper.Map<ProgramEnrollmentDto>(enrollment);
                enrollmentDto.AcademicSessions = _mapper.Map<List<AcademicSessionDto>>(sessions);
                programEnrollments.Add(enrollmentDto);
            }

            return new StudentFullDto
            {
                Student = _mapper.Map<StudentDto>(student),
                SecondaryInfo = _mapper.Map<SecondaryInfoDto>(secondary),
                Disability = _mapper.Map<DisabilityDto>(disability),
                Citizenship = _mapper.Map<CitizenShipDto>(citizenship),
                Ethnicity = _mapper.Map<EthnicityDto>(ethnicity),
                Emergency = _mapper.Map<EmergencyDto>(emergency),
                Addresses = _mapper.Map<List<AddressDto>>(addresses),
                Parents = _mapper.Map<List<ParentDetailDto>>(parents),
                Scholarships = _mapper.Map<ScholarshipDto>(scholarship),
                BankDetails = _mapper.Map<BankDetailDto>(bank),
                StudentExtraInfos = _mapper.Map<StudentExtraInfoDto>(extraInfo.FirstOrDefault()),
                Achievements = _mapper.Map<List<AchievementDto>>(achievements),
                AcademicHistories = _mapper.Map<List<AcademicHistoryDto>>(academicHistories),
                ProgramEnrollments = programEnrollments
            };
        }
    }
}