using FormBackend.Data;
using FormBackend.Models;
using FormBackend.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualBasic;

namespace FormBackend.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Students = new GenericRepository<Student>(_context);
            SecondaryInfos = new GenericRepository<SecondaryInfo>(_context);
            Ethnicities = new GenericRepository<Ethnicity>(_context);
            Emergencies = new GenericRepository<Emergency>(_context);
            Disabilities = new GenericRepository<Disability>(_context);
            CitizenShips = new GenericRepository<CitizenShip>(_context);
            Addresses = new GenericRepository<Address>(_context);
            ParentDetails = new GenericRepository<ParentDetail>(_context);
            ProgramEnrollments = new GenericRepository<ProgramEnrollment>(_context);
            AcademicSessions = new GenericRepository<AcademicSession>(_context);
            AcademicHistories = new GenericRepository<AcademicHistory>(_context);
            Scholarships = new GenericRepository<Scholarship>(_context);
            BankDetails = new GenericRepository<BankDetail>(_context);
            StudentExtraInfos = new GenericRepository<StudentExtraInfo>(_context);
            Achievements = new GenericRepository<Achievement>(_context);
            Declarations = new GenericRepository<Declaration>(_context);


        }

        public IGenericRepository<Student> Students { get; private set; }
        public IGenericRepository<SecondaryInfo> SecondaryInfos { get; private set; }
        public IGenericRepository<Ethnicity> Ethnicities { get; private set; }
        public IGenericRepository<Emergency> Emergencies { get; private set; }
        public IGenericRepository<Disability> Disabilities { get; private set; }
        public IGenericRepository<CitizenShip> CitizenShips { get; private set; }
        public IGenericRepository<Address> Addresses { get; private set; }
        public IGenericRepository<ParentDetail> ParentDetails { get; private set; }
        public IGenericRepository<ProgramEnrollment> ProgramEnrollments { get; private set; }
        public IGenericRepository<AcademicSession> AcademicSessions { get; private set; }
        public IGenericRepository<AcademicHistory> AcademicHistories { get; }
        public IGenericRepository<Scholarship> Scholarships { get; private set; }

        public IGenericRepository<BankDetail> BankDetails { get; private set; }

        public IGenericRepository<StudentExtraInfo> StudentExtraInfos { get; private set; }
        public IGenericRepository<Achievement> Achievements { get; private set; }

        public IGenericRepository<Declaration> Declarations { get; private set; }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
