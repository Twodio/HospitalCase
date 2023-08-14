﻿using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Repositories;
using HospitalCase.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalCase.Tests.Services
{
    public  class HealthcareProviderServiceTests
    {
        [Fact]
        public async Task GetAll_ReturnsAll_HealthcareProviders()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var results = await mockService.GetAllAsync();

            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOne_HealthcareProvider()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var id = 1;
            var firstName = "Jon";
            var lastName = "Doe";
            var type = HealthcareProviderType.Doctor;

            var result = await mockService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(firstName, result.FirstName);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(type, result.Type);
        }

        [Fact]
        public async Task Create_AddsOne_HealthcareProvider()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var newEntry = new HealthcareProvider()
            {
                Id = 4,
                FirstName = "New",
                LastName = "Entry",
                BirthDate = new DateTime(1994, 08, 18),
                PhoneNumber = "1234567890",
                Type = HealthcareProviderType.Doctor
            };

            var result = await mockService.CreateAsync(newEntry);

            var updatedPeopleList = await mockRepository.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, updatedPeopleList.Count());
        }

        [Fact]
        public async Task Delete_RemovesOne_HealthcareProvider()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var result = await mockRepository.DeleteAsync(1);

            var updatedPeopleList = await mockService.GetAllAsync();

            Assert.True(result);
            Assert.Single(updatedPeopleList);
        }

        [Fact]
        public async Task Update_ChangesOne_HealthcareProvider()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var id = 1;

            var foundHealthcareProvider = await mockService.GetByIdAsync(id);

            var updatedFirstName = "changed";

            foundHealthcareProvider.FirstName = updatedFirstName;

            var result = await mockService.UpdateAsync(id, foundHealthcareProvider);

            var updatedHealthcareProvider = await mockService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(updatedFirstName, updatedHealthcareProvider.FirstName);
        }

        private Task<IEnumerable<HealthcareProvider>> GetTestHealthcareProviders()
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

            return Task.FromResult<IEnumerable<HealthcareProvider>>(healthcareproviders);
        }
    }
}
