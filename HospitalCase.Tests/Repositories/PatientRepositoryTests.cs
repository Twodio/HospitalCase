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
    public class PatientRepositoryTests
    {
        [Fact]
        public async Task GetAll_ReturnsAll_Patients_Works()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());

            var results = await mockRepository.GetAllAsync();

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOne_Patient_Works()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());

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
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());

            var newEntry = new Patient()
            {
                Id = 4,
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
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());

            var result = await mockRepository.DeleteAsync(3);

            var updatedPeopleList = await mockRepository.GetAllAsync();

            Assert.True(result);
            Assert.Single(updatedPeopleList);
        }

        [Fact]
        public async Task Update_ChangesOne_Patient_Works()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());

            var id = 3;

            var foundPatient = await mockRepository.GetByIdAsync(id);

            var updatedFirstName = "changed";

            foundPatient.FirstName = updatedFirstName;

            var result = await mockRepository.UpdateAsync(id, foundPatient);

            var updatedPatient = await mockRepository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(updatedFirstName, updatedPatient.FirstName);
        }

        private Task<IEnumerable<Patient>> GetTestPatients()
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

            return Task.FromResult<IEnumerable<Patient>>(patients);
        }
    }
}
