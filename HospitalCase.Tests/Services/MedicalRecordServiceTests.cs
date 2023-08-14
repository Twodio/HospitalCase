using HospitalCase.Application.Interfaces;
using HospitalCase.Application.Services;
using HospitalCase.Domain.Models;
using HospitalCase.Insfrastructure;
using HospitalCase.Insfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HospitalCase.Tests.Services
{
    public class MedicalRecordServiceTests
    {
        private readonly HospitalCaseDbContext _context;

        public MedicalRecordServiceTests()
        {
            // Configure the context to use In-Memory
            var options = new DbContextOptionsBuilder<HospitalCaseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new HospitalCaseDbContext(options);

            // Seed Initial Data
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

            var patients = new HashSet<Patient>()
            {
                new Patient()
                {
                    Id = 3,
                    FirstName = "Adam",
                    LastName = "Willock"
                }
            };

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

            _context.People.AddRange(healthcareproviders);
            _context.People.AddRange(patients);
            _context.MedicalRecords.AddRange(medicalRecords);

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAll_ReturnsAll_MedicalRecords()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_context);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var results = await mockService.GetAllAsync();

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOne_MedicalRecord()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_context);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var id = 1;

            var result = await mockService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_AddsOne_MedicalRecord()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_context);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var newEntry = new MedicalRecord()
            {
                PatientId = 3,
                HealthcareProviderId = 1,
                RecordDate = new DateTime(2023, 08, 13),
                Diagnosis = "Flu"
            };

            var result = await mockService.CreateAsync(newEntry);

            var updatedMedicalRecordsList = await mockService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, updatedMedicalRecordsList.Count());
        }

        [Fact]
        public async Task Delete_RemovesOne_MedicalRecord()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_context);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var result = await mockService.DeleteAsync(1);

            var updatedMedicalRecordsList = await mockService.GetAllAsync();

            Assert.True(result);
            Assert.Single(updatedMedicalRecordsList);
        }

        [Fact]
        public async Task Update_ChangesOne_MedicalRecord()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_context);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var id = 1;

            var foundMedicalRecord = await mockService.GetByIdAsync(id);

            var updatedDiagnosis = "changed";

            foundMedicalRecord.Diagnosis = updatedDiagnosis;

            var result = await mockService.UpdateAsync(id, foundMedicalRecord);

            var updatedMedicalRecord = await mockService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(updatedDiagnosis, updatedMedicalRecord.Diagnosis);
        }
    }
}
