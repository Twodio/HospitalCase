using HospitalCase.Application.Interfaces;
using HospitalCase.Application.Services;
using HospitalCase.Domain.Models;
using HospitalCase.Insfrastructure;
using HospitalCase.Insfrastructure.Repositories;
using HospitalCase.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HospitalCase.Tests.Controllers
{
    public class HealthcareProvidersControllerTests
    {
        private readonly HospitalCaseDbContextFactory _contextFactory;

        public HealthcareProvidersControllerTests()
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

                context.People.AddRange(healthcareproviders);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithHealthcareProviders()
        {
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var healthcareProviders = Assert.IsAssignableFrom<IEnumerable<HealthcareProvider>>(okResult.Value);
            Assert.Equal(2, healthcareProviders.Count());
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult_WithOneHealthcareProvider()
        {
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

            var result = await controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var healthcareProvider = Assert.IsAssignableFrom<HealthcareProvider>(okResult.Value);
            Assert.NotNull(healthcareProvider);
        }

        [Fact]
        public async Task GetById__WithInvalidId_ReturnsBadRequest()
        {
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

            var result = await controller.Get(0);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetById_WithNonExistentId_ReturnsNotFoundResult()
        {
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

            var result = await controller.Get(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

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
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

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
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

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
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

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
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

            var result = await controller.Delete(4);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeletesOneAndReturnsNoContent()
        {
            IHealthcareProviderRepository mockRepository = new HealthcareProviderRepository(_contextFactory);
            var mockLogger = new Mock<ILogger<HealthcareProvidersController>>();

            IHealthcareProviderService mockService = new HealthcareProviderService(mockRepository);

            var controller = new HealthcareProvidersController(mockLogger.Object, mockService);

            var result = await controller.Delete(2);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
