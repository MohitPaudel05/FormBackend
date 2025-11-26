using FormBackend.Models;
using FormBackend.Models.Lookup;
using Microsoft.EntityFrameworkCore;

namespace FormBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<PersonalDetail> PersonalDetails { get; set; }
        public DbSet<PermanentAddress> PermanentAddresses { get; set; }
        public DbSet<TemporaryAddress> TemporaryAddresses { get; set; }
        public DbSet<ParentDetail> ParentDetails { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<FeeDetail> FeeDetails { get; set; }
        public DbSet<Scholarship> Scholarships { get; set; }
        public DbSet<BankDetail> BankDetails { get; set; }
        public DbSet<StudentInterest> StudentInterests { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<HostelTransportDetail> HostelTransportDetails { get; set; }
        public DbSet<Declaration> Declarations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Permanent & Temporary Addresses
          
        }
    }
}