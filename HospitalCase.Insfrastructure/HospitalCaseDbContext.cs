using HospitalCase.Application.Models;
using HospitalCase.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalCase.Insfrastructure
{
    public class HospitalCaseDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Person> People { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public HospitalCaseDbContext(DbContextOptions<HospitalCaseDbContext> options) : base(options)
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
