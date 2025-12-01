using FormBackend.Data;
using FormBackend.Models;
using FormBackend.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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

        }

        public IGenericRepository<Student> Students { get; private set; }
        public IGenericRepository<SecondaryInfo> SecondaryInfos { get; private set; }
        public IGenericRepository<Ethnicity> Ethnicities { get; private set; }
        public IGenericRepository<Emergency> Emergencies { get; private set; }
        public IGenericRepository<Disability> Disabilities { get; private set; }
        public IGenericRepository<CitizenShip> CitizenShips { get; private set; }
        public IGenericRepository<Address> Addresses { get; private set; }
        public IGenericRepository<ParentDetail> ParentDetails { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
