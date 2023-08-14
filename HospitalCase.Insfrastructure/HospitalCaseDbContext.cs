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
            // Mappings for the medical records
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany()
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.HealthcareProvider)
                .WithMany()
                .HasForeignKey(mr => mr.HealthcareProviderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
