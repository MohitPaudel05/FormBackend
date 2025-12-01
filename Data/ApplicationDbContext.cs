using FormBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBackend.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Emergency> Emergencies { get; set; }
        public DbSet<CitizenShip> CitizenShips { get; set; }
        public DbSet<Disability> Disabilities { get; set; }

        public DbSet<Ethnicity> Ethnicity { get; set; }

        public DbSet<SecondaryInfo> SecondaryInfos { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ParentDetail> ParentDetails { get; set; }

    }
}
