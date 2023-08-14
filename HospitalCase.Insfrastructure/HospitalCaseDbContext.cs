using HospitalCase.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalCase.Insfrastructure
{
    public class HospitalCaseDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public HospitalCaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
