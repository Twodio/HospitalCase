using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalCase.Tests.Repositories
{
    public class MedicalRecordTests
    {
        [Fact]
        public async Task GetAll_ReturnsAll_MedicalRecords_Works()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());

            var results = await mockRepository.GetAllAsync();

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOne_MedicalRecord_Works()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());

            var id = 1;

            var result = await mockRepository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_AddsOne_Patient_Works()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());

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

            var result = await mockRepository.CreateAsync(newEntry);

            var updatedMedicalRecordsList = await mockRepository.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, updatedMedicalRecordsList.Count());
        }

        [Fact]
        public async Task Delete_RemovesOne_Patient_Works()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());

            var result = await mockRepository.DeleteAsync(1);

            var updatedMedicalRecordsList = await mockRepository.GetAllAsync();

            Assert.True(result);
            Assert.Single(updatedMedicalRecordsList);
        }

        [Fact]
        public async Task Update_ChangesOne_Patient_Works()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());

            var id = 1;

            var foundMedicalRecord = await mockRepository.GetByIdAsync(id);

            var updatedDiagnosis = "changed";

            foundMedicalRecord.Diagnosis = updatedDiagnosis;

            var result = await mockRepository.UpdateAsync(id, foundMedicalRecord);

            var updatedMedicalRecord = await mockRepository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(updatedDiagnosis, updatedMedicalRecord.Diagnosis);
        }

        private Task<IEnumerable<MedicalRecord>> GetTestMedicalRecords()
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

            return Task.FromResult<IEnumerable<MedicalRecord>>(medicalRecords);
        }
    }
}
