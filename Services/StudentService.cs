using AutoMapper;
using FormBackend.DTOs;
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

            // 2. Permanent Address
            if (studentDto.PermanentAddress != null)
            {
                var permanent = _mapper.Map<PermanentAddress>(studentDto.PermanentAddress);
                permanent.StudentId = studentId;
                await _unitOfWork.PermanentAddresses.AddAsync(permanent);
            }

            // 3. Temporary Address (only if provided)
            if (studentDto.TemporaryAddress != null)
            {
                var temp = _mapper.Map<TemporaryAddress>(studentDto.TemporaryAddress);
                temp.StudentId = studentId;
                await _unitOfWork.TemporaryAddresses.AddAsync(temp);
            }

            // 4. Parents
            if (studentDto.Parents != null)
            {
                foreach (var parentDto in studentDto.Parents)
                {
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
                    var doc = _mapper.Map<Document>(docDto);
                    doc.StudentId = studentId;
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
                PermanentAddress = _mapper.Map<PermanentAddressDto>(await _unitOfWork.PermanentAddresses.FindAsync(p => p.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                TemporaryAddress = _mapper.Map<TemporaryAddressDto>(await _unitOfWork.TemporaryAddresses.FindAsync(t => t.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Parents = _mapper.Map<List<ParentDetailDto>>(await _unitOfWork.ParentDetails.FindAsync(p => p.StudentId == studentId)),
                Enrollment = _mapper.Map<EnrollmentDto>(await _unitOfWork.Enrollments.FindAsync(e => e.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Qualifications = _mapper.Map<List<QualificationDto>>(await _unitOfWork.Qualifications.FindAsync(q => q.StudentId == studentId)),
                Documents = _mapper.Map<List<DocumentDto>>(await _unitOfWork.Documents.FindAsync(d => d.StudentId == studentId)),
                FeeDetail = _mapper.Map<FeeDetailDto>(await _unitOfWork.FeeDetails.FindAsync(f => f.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Scholarship = _mapper.Map<ScholarshipDto>(await _unitOfWork.Scholarships.FindAsync(s => s.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                BankDetail = _mapper.Map<BankDetailDto>(await _unitOfWork.BankDetails.FindAsync(b => b.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Interests = _mapper.Map<List<StudentInterestDto>>(await _unitOfWork.StudentInterests.FindAsync(si => si.StudentId == studentId)),
                Awards = _mapper.Map<List<AwardDto>>(await _unitOfWork.Awards.FindAsync(a => a.StudentId == studentId)),
                HostelTransport = _mapper.Map<HostelTransportDetailDto>(await _unitOfWork.HostelTransportDetails.FindAsync(ht => ht.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault())),
                Declaration = _mapper.Map<DeclarationDto>(await _unitOfWork.Declarations.FindAsync(d => d.StudentId == studentId).ContinueWith(t => t.Result.FirstOrDefault()))
            };

            return dto;
        }
    }
}
