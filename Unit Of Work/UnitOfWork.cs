using FormBackend.Data;
using FormBackend.Models;
using FormBackend.Repositories;

namespace FormBackend.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            PersonalDetails = new GenericRepository<PersonalDetail>(_context);
            PermanentAddresses = new GenericRepository<PermanentAddress>(_context);
            TemporaryAddresses = new GenericRepository<TemporaryAddress>(_context);
            ParentDetails = new GenericRepository<ParentDetail>(_context);
            Enrollments = new GenericRepository<Enrollment>(_context);
            Qualifications = new GenericRepository<Qualification>(_context);
            Documents = new GenericRepository<Document>(_context);
            FeeDetails = new GenericRepository<FeeDetail>(_context);
            Scholarships = new GenericRepository<Scholarship>(_context);
            BankDetails = new GenericRepository<BankDetail>(_context);
            StudentInterests = new GenericRepository<StudentInterest>(_context);
            Awards = new GenericRepository<Award>(_context);
            HostelTransportDetails = new GenericRepository<HostelTransportDetail>(_context);
            Declarations = new GenericRepository<Declaration>(_context);
        }

        public IGenericRepository<PersonalDetail> PersonalDetails { get; private set; }
        public IGenericRepository<PermanentAddress> PermanentAddresses { get; private set; }
        public IGenericRepository<TemporaryAddress> TemporaryAddresses { get; private set; }
        public IGenericRepository<ParentDetail> ParentDetails { get; private set; }
        public IGenericRepository<Enrollment> Enrollments { get; private set; }
        public IGenericRepository<Qualification> Qualifications { get; private set; }
        public IGenericRepository<Document> Documents { get; private set; }
        public IGenericRepository<FeeDetail> FeeDetails { get; private set; }
        public IGenericRepository<Scholarship> Scholarships { get; private set; }
        public IGenericRepository<BankDetail> BankDetails { get; private set; }
        public IGenericRepository<StudentInterest> StudentInterests { get; private set; }
        public IGenericRepository<Award> Awards { get; private set; }
        public IGenericRepository<HostelTransportDetail> HostelTransportDetails { get; private set; }
        public IGenericRepository<Declaration> Declarations { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
