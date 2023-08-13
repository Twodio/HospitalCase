using HospitalCase.WebAPI.Controllers;
using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalCase.Tests.Controllers
{
    public class HealthcareProvidersControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResult_WithHealthcareProviders()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var healthcareProviders = Assert.IsAssignableFrom<IEnumerable<HealthcareProvider>>(okResult.Value);
            Assert.Equal(2, healthcareProviders.Count());
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult_WithOneHealthcareProvider()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var result = await controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var healthcareProvider = Assert.IsAssignableFrom<HealthcareProvider>(okResult.Value);
            Assert.NotNull(healthcareProvider);
        }

        [Fact]
        public async Task GetById__WithInvalidId_ReturnsBadRequest()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var result = await controller.Get(0);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetById_WithNonExistentId_ReturnsNotFoundResult()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var result = await controller.Get(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var newEntry = new HealthcareProvider()
            {
                Id = 1,
                FirstName = "Hellen",
                LastName = "Carlton",
                Type = HealthcareProviderType.Doctor
            };

            var result = await controller.Post(newEntry);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var healthcareProvider = Assert.IsAssignableFrom<HealthcareProvider>(createdAtActionResult.Value);
            Assert.NotNull(healthcareProvider);
        }

        [Fact]
        public async Task Put_MissmatchedId_ReturnsBadRequest()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var newEntry = new HealthcareProvider()
            {
                Id = 1,
                FirstName = "Hellen",
                LastName = "Carlton",
                Type = HealthcareProviderType.Doctor
            };

            var result = await controller.Put(2, newEntry);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Put_NonExistent_ReturnsNotFound()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var newEntry = new HealthcareProvider()
            {
                Id = 4,
                FirstName = "Hellen",
                LastName = "Carlton",
                Type = HealthcareProviderType.Doctor
            };

            var result = await controller.Put(4, newEntry);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Put_UpdatesOneAndReturnsNoContent()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var newEntry = new HealthcareProvider()
            {
                Id = 2,
                FirstName = "Hellen",
                LastName = "Carlton",
                Type = HealthcareProviderType.Nurse
            };

            var result = await controller.Put(2, newEntry);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistent_ReturnsNotFound()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var result = await controller.Delete(4);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeletesOneAndReturnsNoContent()
        {
            var people = await GetTestHealthcareProviders();
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            var controller = new HealthcareProvidersController(mockLogger.Object, mockRepository);

            var result = await controller.Delete(2);

            Assert.IsType<NoContentResult>(result);
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
