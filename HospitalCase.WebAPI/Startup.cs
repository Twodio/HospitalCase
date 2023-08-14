using HospitalCase.Application.Interfaces;
using HospitalCase.Application.Services;
using HospitalCase.Domain.Models;
using HospitalCase.Insfrastructure;
using HospitalCase.Insfrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalCase.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Local reporitory's data for HealthcareProviders
            var healthcareproviders = new HashSet<HealthcareProvider>()
            {
                new HealthcareProvider()
                {
                    Id = 1,
                    FirstName = "Jon",
                    LastName = "Doe",
                    Type = HealthcareProviderType.Doctor
                },
                new HealthcareProvider()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Type = HealthcareProviderType.Doctor
                }
            };

            // Local reporitory's data for Patients
            var patients = new HashSet<Patient>()
            {
                new Patient()
                {
                    Id = 3,
                    FirstName = "Adam",
                    LastName = "Willock"
                }
            };

            // Local reporitory's data for MedicalRecords
            var medicalRecords = new HashSet<MedicalRecord>()
            {
                new MedicalRecord()
                {
                    Id = 1,
                    Patient = patients.Single(p => p.Id == 3),
                    HealthcareProvider = healthcareproviders.Single(hp =>  hp.Id == 1),
                    RecordDate = new DateTime(2023, 08, 13),
                    Diagnosis = "Flu"
                },
                new MedicalRecord()
                {
                    Id = 2,
                    Patient = patients.Single(p => p.Id == 3),
                    HealthcareProvider = healthcareproviders.Single(hp =>  hp.Id == 2),
                    RecordDate = new DateTime(2020, 08, 13),
                    Diagnosis = "Covid-19"
                }
            };

            // Configure Entity Framework Database
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            Action<DbContextOptionsBuilder> optionsBuilder = (options) => options.UseSqlServer(connectionString);

            // Register Entity Framework
            services.AddSingleton<HospitalCaseDbContextFactory>(new HospitalCaseDbContextFactory(optionsBuilder));
            services.AddDbContext<HospitalCaseDbContext>(optionsBuilder);

            // Register repositories
            services.AddSingleton<IHealthcareProviderRepository>(s => new HealthcareProviderRepository(s.GetRequiredService<HospitalCaseDbContextFactory>()));
            services.AddSingleton<IPatientRepository>(s => new PatientRepository(s.GetRequiredService<HospitalCaseDbContextFactory>()));
            services.AddSingleton<IMedicalRecordRepository>(s => new MedicalRecordRepository(s.GetRequiredService<HospitalCaseDbContextFactory>()));

            // Register domain services
            services.AddSingleton<IHealthcareProviderService>(s => new HealthcareProviderService(s.GetRequiredService<IHealthcareProviderRepository>()));
            services.AddSingleton<IPatientService>(s => new PatientService(s.GetRequiredService<IPatientRepository>()));
            services.AddSingleton<IMedicalRecordService>(s => new MedicalRecordService(s.GetRequiredService<IMedicalRecordRepository>()));

            // Register Controllers
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
