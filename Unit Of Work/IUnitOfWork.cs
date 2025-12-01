using FormBackend.Models;
using FormBackend.Repositories;

namespace FormBackend.Unit_Of_Work
{
    public interface IUnitOfWork
    {
        IGenericRepository<Student> Students { get; }
        IGenericRepository<SecondaryInfo> SecondaryInfos { get; }
        IGenericRepository<Ethnicity> Ethnicities { get; }
        IGenericRepository<Emergency> Emergencies { get; }
        IGenericRepository<Disability> Disabilities { get; }
        IGenericRepository<CitizenShip> CitizenShips { get; }

        IGenericRepository<Address> Addresses { get; }

        IGenericRepository<ParentDetail> ParentDetails { get; }
        //enrollment section------------------------------------------------
        IGenericRepository<ProgramEnrollment> ProgramEnrollments { get; }
        IGenericRepository<AcademicSession> AcademicSessions { get; }

        IGenericRepository<AcademicHistory> AcademicHistories { get; }
        //------------------------------------------------------------------
        IGenericRepository<Scholarship> Scholarships { get; }

        IGenericRepository<BankDetail> BankDetails { get; }
        IGenericRepository<StudentExtraInfo> StudentExtraInfos { get; }
        IGenericRepository<Achievement> Achievements { get; }
        IGenericRepository<Declaration> Declarations { get; }
        Task<int> CompleteAsync();
    }
}
