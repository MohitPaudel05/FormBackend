using AutoMapper;
using FormBackend.DTOs;
using FormBackend.Enums;
using FormBackend.Helpers;
using FormBackend.Models;
using FormBackend.Unit_Of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormBackend.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Add full student form
        public async Task AddStudentAsync(StudentFullDto studentDto)
        {
            // 1. PersonalDetail
            var personalDetail = _mapper.Map<PersonalDetail>(studentDto.PersonalDetail);
            await _unitOfWork.PersonalDetails.AddAsync(personalDetail);
            await _unitOfWork.CompleteAsync(); // Save to get Id

            int studentId = personalDetail.Id;

            //2 Address Detail

            if (studentDto.Address != null)
            {
                foreach (var addrDto in studentDto.Address)
                {
                    var address = _mapper.Map<Address>(addrDto);
                    address.StudentId = studentId;
                    await _unitOfWork.Addresses.AddAsync(address);
                }
            }

            // 4. Parents
            if (studentDto.Parents != null)
            {
                foreach (var parentDto in studentDto.Parents)
                {
                    // Save all provided parents
                    var parent = _mapper.Map<ParentDetail>(parentDto);
                    parent.StudentId = studentId;
                    await _unitOfWork.ParentDetails.AddAsync(parent);
                }
            }

            // 5. Enrollment
            if (studentDto.Enrollment != null)
            {
                var enrollment = _mapper.Map<Enrollment>(studentDto.Enrollment);
                enrollment.StudentId = studentId;
                await _unitOfWork.Enrollments.AddAsync(enrollment);
            }

            // 6. Qualifications
            if (studentDto.Qualifications != null)
            {
                foreach (var qDto in studentDto.Qualifications)
                {
                    var qual = _mapper.Map<Qualification>(qDto);
                    qual.StudentId = studentId;
                    await _unitOfWork.Qualifications.AddAsync(qual);
                }
            }
            // 7. Documents
            if (studentDto.Documents != null)
            {
                foreach (var docDto in studentDto.Documents)
                {
                    var doc = new StudentDocument
                    {
                        StudentId = studentId,
                        DocumentType = Enum.Parse<DocumentType>(docDto.DocumentType), // map string to enum
                        FilePath = await FileHelper.SaveFileAsync(docDto.FilePath, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"))
                    };

                    await _unitOfWork.Documents.AddAsync(doc);
                }
            }


            // 8. Fee Details
            if (studentDto.FeeDetail != null)
            {
                var fee = _mapper.Map<FeeDetail>(studentDto.FeeDetail);
                fee.StudentId = studentId;
                await _unitOfWork.FeeDetails.AddAsync(fee);
            }

            // 9. Scholarship (optional)
            if (studentDto.Scholarship != null)
            {
                var scholarship = _mapper.Map<Scholarship>(studentDto.Scholarship);
                scholarship.StudentId = studentId;
                await _unitOfWork.Scholarships.AddAsync(scholarship);
            }

            // 10. Bank Detail (optional)
            if (studentDto.BankDetail != null)
            {
                var bank = _mapper.Map<BankDetail>(studentDto.BankDetail);
                bank.StudentId = studentId;
                await _unitOfWork.BankDetails.AddAsync(bank);
            }

            // 11. Student Interests
            if (studentDto.Interests != null)
            {
                foreach (var interestDto in studentDto.Interests)
                {
                    var interest = _mapper.Map<StudentInterest>(interestDto);
                    interest.StudentId = studentId;
                    await _unitOfWork.StudentInterests.AddAsync(interest);
                }
            }

            // 12. Awards
            if (studentDto.Awards != null)
            {
                foreach (var awardDto in studentDto.Awards)
                {
                    var award = _mapper.Map<Award>(awardDto);
                    award.StudentId = studentId;
                    await _unitOfWork.Awards.AddAsync(award);
                }
            }

            // 13. Hostel/Transport
            if (studentDto.HostelTransport != null)
            {
                var ht = _mapper.Map<HostelTransportDetail>(studentDto.HostelTransport);
                ht.StudentId = studentId;
                await _unitOfWork.HostelTransportDetails.AddAsync(ht);
            }

            // 14. Declaration
            if (studentDto.Declaration != null)
            {
                var decl = _mapper.Map<Declaration>(studentDto.Declaration);
                decl.StudentId = studentId;
                await _unitOfWork.Declarations.AddAsync(decl);
            }
            //15. Disability
            if (studentDto.Disability != null)
            {
                var disability = _mapper.Map<Disability>(studentDto.Disability);
                disability.StudentId = studentId;

                // Optional: ensure HasDisability consistency
                if (!disability.HasDisability)
                {
                    disability.DisabilityType = DisabilityType.None;
                    disability.DisabilityPercentage = null;
                }

                await _unitOfWork.Disabilities.AddAsync(disability);
            }
            //16. Citizenship Info 

            if (studentDto.CitizenshipInfo != null)
            {
                var citizenship = _mapper.Map<CitizenshipInfo>(studentDto.CitizenshipInfo);
                citizenship.StudentId = studentId;
                await _unitOfWork.CitizenshipInfos.AddAsync(citizenship);
            }
            //17. Contact Info
            if (studentDto.ContactInfo != null)
            {
                var contactInfo = _mapper.Map<ContactInfo>(studentDto.ContactInfo);
                contactInfo.StudentId = studentId;
                await _unitOfWork.ContactInfos.AddAsync(contactInfo);
            }

            // 18. Emergency Contacts
           
                foreach (var ecDto in studentDto.EmergencyContacts)
                {
                    var ec = _mapper.Map<EmergencyContact>(ecDto);
                    ec.StudentId = studentId;
                    await _unitOfWork.EmergencyContacts.AddAsync(ec);
                }

                //19. Religion
                if (studentDto.Religion != null)
                {
                    var religion = _mapper.Map<Religion>(studentDto.Religion);
                    religion.StudentId = studentId;
                    await _unitOfWork.Religions.AddAsync(religion);
                }


            


            // Commit all changes in one transaction
            await _unitOfWork.CompleteAsync();
        }

        // Get full student data
        public async Task<StudentFullDto> GetStudentByIdAsync(int studentId)
        {
            var student = await _unitOfWork.PersonalDetails.GetByIdAsync(studentId);
            if (student == null) return null;

            var dto = new StudentFullDto
            {
                PersonalDetail = _mapper.Map<PersonalDetailDto>(student),
              
                Parents = _mapper.Map<List<ParentDetailDto>>(await _unitOfWork.ParentDetails.FindAsync(p => p.StudentId == studentId)),
                Enrollment = _mapper.Map<EnrollmentDto>(await _unitOfWork.Enrollments.FindAsync(e => e.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Qualifications = _mapper.Map<List<QualificationDto>>(await _unitOfWork.Qualifications.FindAsync(q => q.StudentId == studentId)),
                Documents = _mapper.Map<List<StudentDocumentDto>>(await _unitOfWork.Documents.FindAsync(d => d.StudentId == studentId)),
                FeeDetail = _mapper.Map<FeeDetailDto>(await _unitOfWork.FeeDetails.FindAsync(f => f.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Scholarship = _mapper.Map<ScholarshipDto>(await _unitOfWork.Scholarships.FindAsync(s => s.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                BankDetail = _mapper.Map<BankDetailDto>(await _unitOfWork.BankDetails.FindAsync(b => b.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Interests = _mapper.Map<List<StudentInterestDto>>(await _unitOfWork.StudentInterests.FindAsync(si => si.StudentId == studentId)),
                Awards = _mapper.Map<List<AwardDto>>(await _unitOfWork.Awards.FindAsync(a => a.StudentId == studentId)),
                HostelTransport = _mapper.Map<HostelTransportDetailDto>(await _unitOfWork.HostelTransportDetails.FindAsync(ht => ht.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Declaration = _mapper.Map<DeclarationDto>(await _unitOfWork.Declarations.FindAsync(d => d.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Disability = _mapper.Map<DisabilityDto>(await _unitOfWork.Disabilities.FindAsync(d => d.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                CitizenshipInfo = _mapper.Map<CitizenshipInfoDto>(await _unitOfWork.CitizenshipInfos.FindAsync(c => c.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Religion = _mapper.Map<ReligionDto>(await _unitOfWork.Religions.FindAsync(r => r.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                ContactInfo = _mapper.Map<ContactInfoDto>(await _unitOfWork.ContactInfos.FindAsync(c => c.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Address = _mapper.Map<List<AddressDto>>(await _unitOfWork.Addresses.FindAsync(a => a.StudentId == studentId)),
                EmergencyContacts = _mapper.Map<List<EmergencyContactDto>>(await _unitOfWork.EmergencyContacts.FindAsync(ec => ec.StudentId == studentId))
            };

            return dto;
        }

        
}
}
