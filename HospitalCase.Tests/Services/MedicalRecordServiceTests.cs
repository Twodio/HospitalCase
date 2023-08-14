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
        private readonly HospitalCaseDbContextFactory _contextFactory;

        public MedicalRecordServiceTests()
        {
            // Configure the context to use In-Memory
            _contextFactory = new HospitalCaseDbContextFactory(o => o.UseInMemoryDatabase("TestHospitalCaseDB"));

            // Seed Initial Data
            using (var context = _contextFactory.CreateDbContext())
            {
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

                context.People.AddRange(healthcareproviders);
                context.People.AddRange(patients);
                context.MedicalRecords.AddRange(medicalRecords);

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAll_ReturnsAll_MedicalRecords()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_contextFactory);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var results = await mockService.GetAllAsync();

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOne_MedicalRecord()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_contextFactory);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var id = 1;

            var result = await mockService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_AddsOne_MedicalRecord()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_contextFactory);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var newEntry = new MedicalRecord()
            {
                Id = 3,
                Patient = new Patient()
                {
                    Id = 3,
                    FirstName = "Adam",
                    LastName = "Willock"
                },
                HealthcareProvider = new HealthcareProvider()
                {
                    Id = 1,
                    FirstName = "Jon",
                    LastName = "Doe",
                    Type = HealthcareProviderType.Doctor
                },
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
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_contextFactory);

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var result = await mockService.DeleteAsync(1);

            var updatedMedicalRecordsList = await mockService.GetAllAsync();

            Assert.True(result);
            Assert.Single(updatedMedicalRecordsList);
        }

        [Fact]
        public async Task Update_ChangesOne_MedicalRecord()
        {
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(_contextFactory);

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
