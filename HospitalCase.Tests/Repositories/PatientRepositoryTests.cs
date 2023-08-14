using HospitalCase.Application.Interfaces;
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

namespace HospitalCase.Tests.Repositories
{
    public class PatientRepositoryTests
    {
        private readonly HospitalCaseDbContextFactory _contextFactory;

        public PatientRepositoryTests()
        {
            // Configure the context to use In-Memory
            _contextFactory = new HospitalCaseDbContextFactory(o => o.UseInMemoryDatabase("TestHospitalCaseDB"));

            // Seed Initial Data
            using (var context = _contextFactory.CreateDbContext())
            {
                var patients = new HashSet<Patient>()
                {
                    new Patient()
                    {
                        Id = 3,
                        FirstName = "Adam",
                        LastName = "Willock"
                    },
                    new Patient()
                    {
                        Id = 4,
                        FirstName = "Mariah",
                        LastName = "Leaft"
                    }
                };

                context.People.AddRange(patients);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAll_ReturnsAll_Patients_Works()
        {
            IPatientRepository mockRepository = new PatientRepository(_contextFactory);

            var results = await mockRepository.GetAllAsync();

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOne_Patient_Works()
        {
            IPatientRepository mockRepository = new PatientRepository(_contextFactory);

            var id = 3;
            var firstName = "Adam";
            var lastName = "Willock";

            var result = await mockRepository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(firstName, result.FirstName);
            Assert.Equal(lastName, result.LastName);
        }

        [Fact]
        public async Task Create_AddsOne_Patient_Works()
        {
            IPatientRepository mockRepository = new PatientRepository(_contextFactory);

            var newEntry = new Patient()
            {
                FirstName = "New",
                LastName = "Entry",
                BirthDate = new DateTime(1994, 08, 18),
                PhoneNumber = "1234567890"
            };

            var result = await mockRepository.CreateAsync(newEntry);

            var updatedPeopleList = await mockRepository.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, updatedPeopleList.Count());
        }

        [Fact]
        public async Task Delete_RemovesOne_Patient_Works()
        {
            IPatientRepository mockRepository = new PatientRepository(_contextFactory);

            var result = await mockRepository.DeleteAsync(3);

            var updatedPeopleList = await mockRepository.GetAllAsync();

            Assert.True(result);
            Assert.Single(updatedPeopleList);
        }

        [Fact]
        public async Task Update_ChangesOne_Patient_Works()
        {
            IPatientRepository mockRepository = new PatientRepository(_contextFactory);

            var id = 3;

            var foundPatient = await mockRepository.GetByIdAsync(id);

            var updatedFirstName = "changed";

            foundPatient.FirstName = updatedFirstName;

            var result = await mockRepository.UpdateAsync(id, foundPatient);

            var updatedPatient = await mockRepository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(updatedFirstName, updatedPatient.FirstName);
        }
    }
}
