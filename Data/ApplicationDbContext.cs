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
        public DbSet<ProgramEnrollment> ProgramEnrollments { get; set; }
        public DbSet<AcademicSession> AcademicSessions { get; set; }

        public DbSet<AcademicHistory> AcademicHistories { get; set; }

        public DbSet<Scholarship> Scholarships { get; set; }

        public DbSet<BankDetail> Banks { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<StudentExtraInfo> StudentExtraInfos { get; set; }
        
        public DbSet<Declaration> Declarations { get; set; }

        }
}
