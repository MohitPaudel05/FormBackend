using FormBackend.Models;
using FormBackend.Repositories;

namespace FormBackend.Unit_Of_Work
{
    public interface IUnitOfWork
    {
        IGenericRepository<PersonalDetail> PersonalDetails { get; }
       
        IGenericRepository<ParentDetail> ParentDetails { get; }
        IGenericRepository<Enrollment> Enrollments { get; }
        IGenericRepository<Qualification> Qualifications { get; }
        IGenericRepository<StudentDocument> Documents { get; }
        IGenericRepository<FeeDetail> FeeDetails { get; }
        IGenericRepository<Scholarship> Scholarships { get; }
        IGenericRepository<BankDetail> BankDetails { get; }
        IGenericRepository<StudentInterest> StudentInterests { get; }
        IGenericRepository<Award> Awards { get; }
        IGenericRepository<HostelTransportDetail> HostelTransportDetails { get; }
        IGenericRepository<Declaration> Declarations { get; }

        IGenericRepository<CitizenshipInfo> CitizenshipInfos { get; }

        IGenericRepository<Disability> Disabilities { get; }
        IGenericRepository<ContactInfo> ContactInfos { get; }
        IGenericRepository<EmergencyContact> EmergencyContacts { get; }

        

        IGenericRepository<Religion> Religions { get; }

        IGenericRepository<Address> Addresses { get; }

        Task<int> CompleteAsync();

        
    }
}
