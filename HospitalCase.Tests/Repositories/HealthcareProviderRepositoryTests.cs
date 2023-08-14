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
    public class HealthcareProviderRepositoryTests
    {
        private readonly HospitalCaseDbContext _context;

        public HealthcareProviderRepositoryTests()
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
                        FirstName = "Jon",
                        LastName = "Doe",
                        Type = HealthcareProviderType.Doctor
                    },
                    new HealthcareProvider()
                    {
                        FirstName = "Jane",
                        LastName = "Smith",
                        Type = HealthcareProviderType.Doctor
                    }
                };

            _context.People.AddRange(healthcareproviders);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAll_ReturnsAll_HealthcareProviders()
        {
            var mockRepository = new HealthcareProviderRepository(_context);

            var results = await mockRepository.GetAllAsync();

            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOne_HealthcareProvider()
        {
            var entry = new HealthcareProvider()
            {
                Id = 1,
                FirstName = "Jon",
                LastName = "Doe",
                Type = HealthcareProviderType.Doctor
            };

            var mockRepository = new HealthcareProviderRepository(_context);

            var result = await mockRepository.GetByIdAsync(entry.Id);

            Assert.NotNull(result);
            Assert.Equal(entry.Id, result.Id);
            Assert.Equal(entry.FirstName, result.FirstName);
            Assert.Equal(entry.LastName, result.LastName);
            Assert.Equal(entry.Type, result.Type);
        }

        [Fact]
        public async Task Create_AddsOne_HealthcareProvider()
        {
            var newEntry = new HealthcareProvider()
            {
                FirstName = "New",
                LastName = "Entry",
                BirthDate = new DateTime(1994, 08, 18),
                PhoneNumber = "1234567890",
                Type = HealthcareProviderType.Doctor
            };

            var mockRepository = new HealthcareProviderRepository(_context);

            var result = await mockRepository.CreateAsync(newEntry);

            var updatedPeopleList = await mockRepository.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, updatedPeopleList.Count());
        }

        [Fact]
        public async Task Delete_RemovesOne_HealthcareProvider()
        {
            var mockRepository = new HealthcareProviderRepository(_context);

            var result = await mockRepository.DeleteAsync(1);

            var updatedPeopleList = await mockRepository.GetAllAsync();

            Assert.True(result);
            Assert.Single(updatedPeopleList);
        }

        [Fact]
        public async Task Update_ChangesOne_HealthcareProvider()
        {
            var entry = new HealthcareProvider()
            {
                Id = 1,
                FirstName = "Changed",
                LastName = "Doe",
                Type = HealthcareProviderType.Doctor
            };

            var mockRepository = new HealthcareProviderRepository(_context);

            var result = await mockRepository.UpdateAsync(entry.Id, entry);

            Assert.NotNull(result);
            Assert.Equal(entry.FirstName, result.FirstName);
        }
    }
}
