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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HospitalCase.Tests.Controllers
{
    public class PatientsControllerTests
    {
        private readonly HospitalCaseDbContext _context;

        public PatientsControllerTests()
        {
            var options = new DbContextOptionsBuilder<HospitalCaseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            _context = new HospitalCaseDbContext(options);

            // Seed Initial Data
            var patients = new HashSet<Patient>()
            {
                new Patient()
                {
                    FirstName = "Adam",
                    LastName = "Willock"
                },
                new Patient()
                {
                    FirstName = "Mariah",
                    LastName = "Leaft"
                }
            };

            _context.People.AddRange(patients);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithPatients()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var patients = Assert.IsAssignableFrom<IEnumerable<Patient>>(okResult.Value);
            Assert.Equal(2, patients.Count());
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult_WithOnePatient()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var patient = Assert.IsAssignableFrom<Patient>(okResult.Value);
            Assert.NotNull(patient);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsBadRequest()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Get(0);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetById_WithNonExistentId_ReturnsNotFoundResult()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Get(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var newEntry = new Patient()
            {
                Id = 3,
                FirstName = "Jonny",
                LastName = "Luck"
            };

            var result = await controller.Post(newEntry);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var patient = Assert.IsAssignableFrom<Patient>(createdAtActionResult.Value);
            Assert.NotNull(patient);
        }

        [Fact]
        public async Task Put_MissmatchedId_ReturnsBadRequest()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var newEntry = new Patient()
            {
                Id = 3,
                FirstName = "Jonny",
                LastName = "Luck"
            };

            var result = await controller.Put(2, newEntry);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Put_NonExistent_ReturnsNotFound()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var newEntry = new Patient()
            {
                Id = 5,
                FirstName = "Jonny",
                LastName = "Luck"
            };

            var result = await controller.Put(5, newEntry);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Put_UpdatesOneAndReturnsNoContent()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var newEntry = new Patient()
            {
                Id = 2,
                FirstName = "Jonny",
                LastName = "Luck"
            };

            var result = await controller.Put(2, newEntry);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistent_ReturnsNotFound()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Delete(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeletesOneAndReturnsNoContent()
        {
            IPatientRepository mockRepository = new PatientRepository(_context);

            IPatientService mockService = new PatientService(mockRepository);
            var mockLogger = new Mock<ILogger<PatientsController>>();

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Delete(2);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
