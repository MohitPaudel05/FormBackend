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

        Task<int> CompleteAsync();
    }
}
