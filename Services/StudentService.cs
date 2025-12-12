using AutoMapper;
using FormBackend.DTOs;
using FormBackend.Enums;
using FormBackend.Helpers;
using FormBackend.Models;
using FormBackend.Unit_Of_Work;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
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
                    "images"
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
            if (dto.Addresses != null && dto.Addresses.Any())
            {
                var permanentDto = dto.Addresses.FirstOrDefault(a => a.AddressType == "Permanent");
                var temporaryDto = dto.Addresses.FirstOrDefault(a =>
                    a.AddressType == "Temporary" || a.AddressType == "SameAsPermanent");

                if (permanentDto != null)
                {
                    var permanent = _mapper.Map<Address>(permanentDto);
                    permanent.StudentId = studentId;
                    permanent.AddressType = AddressType.Permanent;
                    await _unitOfWork.Addresses.AddAsync(permanent);
                }

                if (temporaryDto != null)
                {
                    bool isSame = permanentDto != null &&
                                  permanentDto.Province == temporaryDto.Province &&
                                  permanentDto.District == temporaryDto.District &&
                                  permanentDto.Municipality == temporaryDto.Municipality &&
                                  permanentDto.WardNumber == temporaryDto.WardNumber &&
                                  permanentDto.Tole == temporaryDto.Tole &&
                                  permanentDto.HouseNumber == temporaryDto.HouseNumber;

                    var addressToSave = isSame ? permanentDto : temporaryDto;

                    var entity = _mapper.Map<Address>(addressToSave);
                    entity.StudentId = studentId;
                    entity.AddressType = isSame ? AddressType.SameAsPermanent : AddressType.Temporary;

                    await _unitOfWork.Addresses.AddAsync(entity);
                }
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
                        acDto.MarksheetPath = await FileHelper.SaveDocumentAsync(acDto.Marksheet, _wwwrootPath, "academic");

                    if (acDto.Provisional != null)
                        acDto.ProvisionalPath = await FileHelper.SaveDocumentAsync(acDto.Provisional, _wwwrootPath, "academic");

                    // NEW: Save photo, signature, and character certificate
                    if (acDto.Photo != null)
                        acDto.PhotoPath = await FileHelper.SaveImageAsync(acDto.Photo, _wwwrootPath, "academic");

                    if (acDto.Signature != null)
                        acDto.SignaturePath = await FileHelper.SaveSignatureAsync(acDto.Signature, _wwwrootPath, "academic");

                    if (acDto.CharacterCertificate != null)
                        acDto.CharacterCertificatePath = await FileHelper.SaveDocumentAsync(acDto.CharacterCertificate, _wwwrootPath, "academic");

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
            var parents = await _unitOfWork.ParentDetails.FindAsync(p => p.StudentId == id);
            var scholarship = (await _unitOfWork.Scholarships.FindAsync(s => s.StudentId == id)).FirstOrDefault();
            var bank = (await _unitOfWork.BankDetails.FindAsync(b => b.StudentId == id)).FirstOrDefault();
            var extraInfo = (await _unitOfWork.StudentExtraInfos.FindAsync(e => e.StudentId == id)).FirstOrDefault();
            var achievements = await _unitOfWork.Achievements.FindAsync(a => a.StudentId == id);
            var declaration = (await _unitOfWork.Declarations.FindAsync(d => d.StudentId == id)).FirstOrDefault();

            var enrollmentData = (await _unitOfWork.ProgramEnrollments.FindAsync(e => e.StudentId == id)).FirstOrDefault();

            
            List<AcademicSession> academicSessions = new List<AcademicSession>();
            if (enrollmentData != null)
            {
                academicSessions = (await _unitOfWork.AcademicSessions.FindAsync(
                    a => a.ProgramEnrollmentId == enrollmentData.Id
                )).ToList();

                
                enrollmentData.AcademicSessions = academicSessions;
            }

            return new StudentFullDto
            {
                Student = _mapper.Map<StudentDto>(student),
                SecondaryInfo = _mapper.Map<SecondaryInfoDto>(secondary) ?? new SecondaryInfoDto(),
                Disability = _mapper.Map<DisabilityDto>(disability) ?? new DisabilityDto(),
                Citizenship = _mapper.Map<CitizenShipDto>(citizenship) ?? new CitizenShipDto(),
                Ethnicity = _mapper.Map<EthnicityDto>(ethnicity) ?? new EthnicityDto(),
                Emergency = _mapper.Map<EmergencyDto>(emergency) ?? new EmergencyDto(),
                Addresses = _mapper.Map<List<AddressDto>>(addresses) ?? new List<AddressDto>(),
                Parents = _mapper.Map<List<ParentDetailDto>>(parents) ?? new List<ParentDetailDto>(),
                Scholarships = _mapper.Map<ScholarshipDto>(scholarship) ?? new ScholarshipDto(),
                BankDetails = _mapper.Map<BankDetailDto>(bank) ?? new BankDetailDto(),
                StudentExtraInfos = _mapper.Map<StudentExtraInfoDto>(extraInfo) ?? new StudentExtraInfoDto(),
                Achievements = _mapper.Map<List<AchievementDto>>(achievements) ?? new List<AchievementDto>(),
                AcademicHistories = _mapper.Map<List<AcademicHistoryDto>>(academicHistories) ?? new List<AcademicHistoryDto>(),
              
                ProgramEnrollments = _mapper.Map<ProgramEnrollmentDto>(enrollmentData) ?? new ProgramEnrollmentDto(),
                Declaration = _mapper.Map<DeclarationDto>(declaration) ?? new DeclarationDto()
            };
        }

        //delete student by id

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null)
                return false;

            _unitOfWork.Students.Delete(student);

            await _unitOfWork.CompleteAsync();
            return true;
        }

        //get all students
        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _unitOfWork.Students.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }
        public async Task<StudentFullDto?> UpdateStudentAsync(int studentId, StudentFullDto dto)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
                return null;

            // ------------ UPDATE STUDENT TABLE ------------
            if (dto.Student != null)
            {
                // Handle new image upload
                if (dto.Student.Image != null)
                {
                    dto.Student.ImagePath = await FileHelper.SaveImageAsync(
                        dto.Student.Image,
                        _wwwrootPath,
                        "images"
                    );
                }
                else
                {
                    // Keep old path
                    dto.Student.ImagePath = student.ImagePath;
                }

                _mapper.Map(dto.Student, student);
                _unitOfWork.Students.Update(student);
            }

            // ------------ UPDATE SECONDARY INFO ------------
            if (dto.SecondaryInfo != null)
            {
                var secondary = (await _unitOfWork.SecondaryInfos.FindAsync(x => x.StudentId == studentId))
                    .FirstOrDefault();
                if (secondary != null)
                {
                    _mapper.Map(dto.SecondaryInfo, secondary);
                    _unitOfWork.SecondaryInfos.Update(secondary);
                }
                else
                {
                    var newSecondary = _mapper.Map<SecondaryInfo>(dto.SecondaryInfo);
                    newSecondary.StudentId = studentId;
                    await _unitOfWork.SecondaryInfos.AddAsync(newSecondary);
                }
            }

            // ------------ UPDATE DISABILITY ------------
            if (dto.Disability != null)
            {
                var disability = (await _unitOfWork.Disabilities.FindAsync(x => x.StudentId == studentId))
                    .FirstOrDefault();
                if (disability != null)
                {
                    _mapper.Map(dto.Disability, disability);
                    _unitOfWork.Disabilities.Update(disability);
                }
                else
                {
                    var newDisability = _mapper.Map<Disability>(dto.Disability);
                    newDisability.StudentId = studentId;
                    await _unitOfWork.Disabilities.AddAsync(newDisability);
                }
            }

            //----Update CITIZENSHIP----

            if (dto.Citizenship != null)
            {
                var citizenship = (await _unitOfWork.CitizenShips.FindAsync(x => x.StudentId == studentId))
                    .FirstOrDefault();
                if (citizenship != null)
                {
                    // Handle new front photo upload
                    if (dto.Citizenship.CitizenshipFrontPhoto != null)
                    {
                        dto.Citizenship.CitizenshipFrontPhotoPath = await FileHelper.SaveImageAsync(
                            dto.Citizenship.CitizenshipFrontPhoto,
                            _wwwrootPath,
                            "citizenship"
                        );
                    }
                    else
                    {
                        // Keep old path
                        dto.Citizenship.CitizenshipFrontPhotoPath = citizenship.CitizenshipFrontPhotoPath;
                    }
                    // Handle new back photo upload
                    if (dto.Citizenship.CitizenshipBackPhoto != null)
                    {
                        dto.Citizenship.CitizenshipBackPhotoPath = await FileHelper.SaveImageAsync(
                            dto.Citizenship.CitizenshipBackPhoto,
                            _wwwrootPath,
                            "citizenship"
                        );
                    }
                    else
                    {
                        // Keep old path
                        dto.Citizenship.CitizenshipBackPhotoPath = citizenship.CitizenshipBackPhotoPath;
                    }
                    _mapper.Map(dto.Citizenship, citizenship);
                    _unitOfWork.CitizenShips.Update(citizenship);
                }
                else
                {
                    var newCitizenship = _mapper.Map<CitizenShip>(dto.Citizenship);
                    newCitizenship.StudentId = studentId;
                    await _unitOfWork.CitizenShips.AddAsync(newCitizenship);
                }
            }

            // ------------ UPDATE ETHNICITY ------------
            if (dto.Ethnicity != null)
            {
                var ethnicity = (await _unitOfWork.Ethnicities.FindAsync(x => x.StudentId == studentId))
                    .FirstOrDefault();
                if (ethnicity != null)
                {
                    _mapper.Map(dto.Ethnicity, ethnicity);
                    _unitOfWork.Ethnicities.Update(ethnicity);
                }
                else
                {
                    var newEthnicity = _mapper.Map<Ethnicity>(dto.Ethnicity);
                    newEthnicity.StudentId = studentId;
                    await _unitOfWork.Ethnicities.AddAsync(newEthnicity);
                }
            }

            // ------------ UPDATE EMERGENCY ------------
            if (dto.Emergency != null)
            {
                var emergency = (await _unitOfWork.Emergencies.FindAsync(x => x.StudentId == studentId))
                    .FirstOrDefault();
                if (emergency != null)
                {
                    _mapper.Map(dto.Emergency, emergency);
                    _unitOfWork.Emergencies.Update(emergency);
                }
                else
                {
                    var newEmergency = _mapper.Map<Emergency>(dto.Emergency);
                    newEmergency.StudentId = studentId;
                    await _unitOfWork.Emergencies.AddAsync(newEmergency);
                }
            }
            //address
            if (dto.Addresses != null && dto.Addresses.Any())
            {
                var sameAsPermanentDto = dto.Addresses.FirstOrDefault(a => a.AddressType == "SameAsPermanent");
                var permanentDto = dto.Addresses.FirstOrDefault(a => a.AddressType == "Permanent");
                var temporaryDto = dto.Addresses.FirstOrDefault(a => a.AddressType == "Temporary");

                // Fetch all existing addresses for this student
                var existingAddresses = (await _unitOfWork.Addresses
                    .FindAsync(a => a.StudentId == studentId))
                    .ToList();

                if (sameAsPermanentDto != null)
                {
                    // User wants SameAsPermanent
                    // Delete existing Permanent (1) and Temporary (2)
                    foreach (var addr in existingAddresses.Where(a => a.AddressType == AddressType.Permanent
                                                                    || a.AddressType == AddressType.Temporary))
                    {
                        _unitOfWork.Addresses.Delete(addr);
                    }

                    // Reuse existing SameAsPermanent if exists
                    var sameAddress = existingAddresses.FirstOrDefault(a => a.AddressType == AddressType.SameAsPermanent);
                    if (sameAddress != null)
                    {
                        // Update existing row
                        _mapper.Map(sameAsPermanentDto, sameAddress);
                    }
                    else
                    {
                        // Add new row only if none exists
                        sameAddress = _mapper.Map<Address>(sameAsPermanentDto);
                        sameAddress.StudentId = studentId;
                        sameAddress.AddressType = AddressType.SameAsPermanent;
                        await _unitOfWork.Addresses.AddAsync(sameAddress);
                    }
                }
                else
                {
                    // User wants separate Permanent and Temporary

                    // Delete existing SameAsPermanent (3)
                    foreach (var addr in existingAddresses.Where(a => a.AddressType == AddressType.SameAsPermanent))
                    {
                        _unitOfWork.Addresses.Delete(addr);
                    }

                    // Permanent
                    if (permanentDto != null)
                    {
                        var permanentAddress = existingAddresses.FirstOrDefault(a => a.AddressType == AddressType.Permanent);
                        if (permanentAddress != null)
                            _mapper.Map(permanentDto, permanentAddress); // update existing
                        else
                        {
                            // Add only if none exists
                            permanentAddress = _mapper.Map<Address>(permanentDto);
                            permanentAddress.StudentId = studentId;
                            permanentAddress.AddressType = AddressType.Permanent;
                            await _unitOfWork.Addresses.AddAsync(permanentAddress);
                        }
                    }

                    // Temporary
                    if (temporaryDto != null)
                    {
                        var tempAddress = existingAddresses.FirstOrDefault(a => a.AddressType == AddressType.Temporary);
                        if (tempAddress != null)
                            _mapper.Map(temporaryDto, tempAddress); // update existing
                        else
                        {
                            // Add only if none exists
                            tempAddress = _mapper.Map<Address>(temporaryDto);
                            tempAddress.StudentId = studentId;
                            tempAddress.AddressType = AddressType.Temporary;
                            await _unitOfWork.Addresses.AddAsync(tempAddress);
                        }
                    }
                }
            }






            // ------------ UPDATE SCHOLARSHIP (ONLY if has values) ------------
            if (dto.Scholarships != null && !string.IsNullOrEmpty(dto.Scholarships.ScholarshipType))
            {
                var scholarship = (await _unitOfWork.Scholarships.FindAsync(s => s.StudentId == studentId))
                    .FirstOrDefault();
                if (scholarship != null)
                {
                    _mapper.Map(dto.Scholarships, scholarship);
                    _unitOfWork.Scholarships.Update(scholarship);
                }
                else
                {
                    var newScholarship = _mapper.Map<Scholarship>(dto.Scholarships);
                    newScholarship.StudentId = studentId;
                    await _unitOfWork.Scholarships.AddAsync(newScholarship);
                }
            }

            // ------------ UPDATE BANK DETAILS (ONLY if has values) ------------
            if (dto.BankDetails != null && !string.IsNullOrEmpty(dto.BankDetails.AccountHolderName))
            {
                var bank = (await _unitOfWork.BankDetails.FindAsync(b => b.StudentId == studentId))
                    .FirstOrDefault();
                if (bank != null)
                {
                    _mapper.Map(dto.BankDetails, bank);
                    _unitOfWork.BankDetails.Update(bank);
                }
                else
                {
                    var newBank = _mapper.Map<BankDetail>(dto.BankDetails);
                    newBank.StudentId = studentId;
                    await _unitOfWork.BankDetails.AddAsync(newBank);
                }
            }

            // ------------ UPDATE EXTRA INFO (ONLY if has values) ------------
           
            if (dto.StudentExtraInfos != null &&
                (!string.IsNullOrEmpty(dto.StudentExtraInfos.HostellerStatus) ||
                 !string.IsNullOrEmpty(dto.StudentExtraInfos.Transportation) ||
                 (dto.StudentExtraInfos.ExtracurricularInterests != null && dto.StudentExtraInfos.ExtracurricularInterests.Any())))
            {
                var extra = (await _unitOfWork.StudentExtraInfos.FindAsync(e => e.StudentId == studentId))
                    .FirstOrDefault();
                if (extra != null)
                {
                    _mapper.Map(dto.StudentExtraInfos, extra);
                    _unitOfWork.StudentExtraInfos.Update(extra);
                }
                else
                {
                    var newExtra = _mapper.Map<StudentExtraInfo>(dto.StudentExtraInfos);
                    newExtra.StudentId = studentId;
                    await _unitOfWork.StudentExtraInfos.AddAsync(newExtra);
                }
            }

            // ------------ UPDATE ACHIEVEMENTS (Delete old, add new) ------------
            if (dto.Achievements != null && dto.Achievements.Any())
            {
                var existingAchievements = await _unitOfWork.Achievements.FindAsync(a => a.StudentId == studentId);
                foreach (var achievement in existingAchievements)
                {
                    _unitOfWork.Achievements.Delete(achievement);
                }

                foreach (var achDto in dto.Achievements)
                {
                    var newAch = _mapper.Map<Achievement>(achDto);
                    newAch.StudentId = studentId;
                    await _unitOfWork.Achievements.AddAsync(newAch);
                }
            }

            // ------------ UPDATE ACADEMIC HISTORY (Delete old, add new) ------------
            if (dto.AcademicHistories != null && dto.AcademicHistories.Any())
            {
                var existingAcademics = await _unitOfWork.AcademicHistories.FindAsync(a => a.StudentId == studentId);
                foreach (var academic in existingAcademics)
                {
                    _unitOfWork.AcademicHistories.Delete(academic);
                }

                foreach (var acDto in dto.AcademicHistories)
                {
                    if (string.IsNullOrEmpty(acDto.Qualification) && string.IsNullOrEmpty(acDto.Board))
                        continue;

                    // Save uploaded files if they exist
                    if (acDto.Marksheet != null)
                        acDto.MarksheetPath = await FileHelper.SaveDocumentAsync(acDto.Marksheet, _wwwrootPath, "academic");

                    if (acDto.Provisional != null)
                        acDto.ProvisionalPath = await FileHelper.SaveDocumentAsync(acDto.Provisional, _wwwrootPath, "academic");

                    if (acDto.Photo != null)
                        acDto.PhotoPath = await FileHelper.SaveImageAsync(acDto.Photo, _wwwrootPath, "academic");

                    if (acDto.Signature != null)
                        acDto.SignaturePath = await FileHelper.SaveSignatureAsync(acDto.Signature, _wwwrootPath, "academic");

                    if (acDto.CharacterCertificate != null)
                        acDto.CharacterCertificatePath = await FileHelper.SaveDocumentAsync(acDto.CharacterCertificate, _wwwrootPath, "academic");

                    var newAc = _mapper.Map<AcademicHistory>(acDto);
                    newAc.StudentId = studentId;
                    await _unitOfWork.AcademicHistories.AddAsync(newAc);
                }
            }

            // ------------ UPDATE PROGRAM ENROLLMENT & ACADEMIC SESSIONS (ONLY if has values) ------------
            if (dto.ProgramEnrollments != null && !string.IsNullOrEmpty(dto.ProgramEnrollments.Faculty))
            {
                var enrollment = (await _unitOfWork.ProgramEnrollments.FindAsync(e => e.StudentId == studentId))
                    .FirstOrDefault();

                if (enrollment != null)
                {
                    _mapper.Map(dto.ProgramEnrollments, enrollment);
                    _unitOfWork.ProgramEnrollments.Update(enrollment);
                }
                else
                {
                    enrollment = _mapper.Map<ProgramEnrollment>(dto.ProgramEnrollments);
                    enrollment.StudentId = studentId;
                    enrollment.EnrollmentDate = DateTime.Now;
                    await _unitOfWork.ProgramEnrollments.AddAsync(enrollment);
                }

                await _unitOfWork.CompleteAsync();

                // Update Academic Sessions - Delete old and add new
                if (dto.ProgramEnrollments.AcademicSessions != null && dto.ProgramEnrollments.AcademicSessions.Any())
                {
                    var existingSessions = await _unitOfWork.AcademicSessions.FindAsync(s => s.ProgramEnrollmentId == enrollment.Id);
                    foreach (var session in existingSessions)
                    {
                        _unitOfWork.AcademicSessions.Delete(session);
                    }

                    foreach (var sessionDto in dto.ProgramEnrollments.AcademicSessions)
                    {
                        var session = _mapper.Map<AcademicSession>(sessionDto);
                        session.ProgramEnrollmentId = enrollment.Id;
                        await _unitOfWork.AcademicSessions.AddAsync(session);
                    }
                }
            }

            // ------------ UPDATE DECLARATION (ONLY if has values) ------------
            if (dto.Declaration != null && dto.Declaration.IsAgreed && !string.IsNullOrEmpty(dto.Declaration.Place))
            {
                var declaration = (await _unitOfWork.Declarations.FindAsync(d => d.StudentId == studentId))
                    .FirstOrDefault();
                if (declaration != null)
                {
                    _mapper.Map(dto.Declaration, declaration);
                    _unitOfWork.Declarations.Update(declaration);
                }
                else
                {
                    var newDeclaration = _mapper.Map<Declaration>(dto.Declaration);
                    newDeclaration.StudentId = studentId;
                    newDeclaration.DateOfApplication = DateOnly.FromDateTime(DateTime.Now);
                    await _unitOfWork.Declarations.AddAsync(newDeclaration);
                }
            }

            // SAVE ALL CHANGES
            await _unitOfWork.CompleteAsync();

            return await GetStudentByIdAsync(studentId);
        }


    }
}