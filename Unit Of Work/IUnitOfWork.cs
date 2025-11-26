using FormBackend.Models;
using FormBackend.Repositories;

namespace FormBackend.Unit_Of_Work
{
    public interface IUnitOfWork
    {
        IGenericRepository<PersonalDetail> PersonalDetails { get; }
        IGenericRepository<PermanentAddress> PermanentAddresses { get; }
        IGenericRepository<TemporaryAddress> TemporaryAddresses { get; }
        IGenericRepository<ParentDetail> ParentDetails { get; }
        IGenericRepository<Enrollment> Enrollments { get; }
        IGenericRepository<Qualification> Qualifications { get; }
        IGenericRepository<Document> Documents { get; }
        IGenericRepository<FeeDetail> FeeDetails { get; }
        IGenericRepository<Scholarship> Scholarships { get; }
        IGenericRepository<BankDetail> BankDetails { get; }
        IGenericRepository<StudentInterest> StudentInterests { get; }
        IGenericRepository<Award> Awards { get; }
        IGenericRepository<HostelTransportDetail> HostelTransportDetails { get; }
        IGenericRepository<Declaration> Declarations { get; }

        Task<int> CompleteAsync();

        
    }
}
