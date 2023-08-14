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
    public class PatientServiceTests
    {
        private readonly HospitalCaseDbContext _context;

        public PatientServiceTests()
        {
            // Configure the context to use In-Memory
            var options = new DbContextOptionsBuilder<HospitalCaseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new HospitalCaseDbContext(options);

            // Seed Initial Data
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

            _context.People.AddRange(patients);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAll_ReturnsAll_Patients()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);

            var results = await mockService.GetAllAsync();

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOne_Patient()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);

            var id = 3;
            var firstName = "Adam";
            var lastName = "Willock";

            var result = await mockService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(firstName, result.FirstName);
            Assert.Equal(lastName, result.LastName);
        }

        [Fact]
        public async Task Create_AddsOne_Patient()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);

            var newEntry = new Patient()
            {
                FirstName = "New",
                LastName = "Entry",
                BirthDate = new DateTime(1994, 08, 18),
                PhoneNumber = "1234567890"
            };

            var result = await mockService.CreateAsync(newEntry);

            var updatedPeopleList = await mockService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, updatedPeopleList.Count());
        }

        [Fact]
        public async Task Delete_RemovesOne_Patient()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);

            var result = await mockService.DeleteAsync(3);

            var updatedPeopleList = await mockService.GetAllAsync();

            Assert.True(result);
            Assert.Single(updatedPeopleList);
        }

        [Fact]
        public async Task Update_ChangesOne_Patient()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);

            var id = 3;

            var foundPatient = await mockService.GetByIdAsync(id);

            var updatedFirstName = "changed";

            foundPatient.FirstName = updatedFirstName;

            var result = await mockService.UpdateAsync(id, foundPatient);

            var updatedPatient = await mockService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(updatedFirstName, updatedPatient.FirstName);
        }
    }
}
