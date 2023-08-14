using HospitalCase.WebAPI.Controllers;
using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Repositories;
using HospitalCase.WebAPI.Services;
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
    public class PatientsControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResult_WithPatients()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var patients = Assert.IsAssignableFrom<IEnumerable<Patient>>(okResult.Value);
            Assert.Equal(2, patients.Count());
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult_WithOnePatient()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Get(3);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var patient = Assert.IsAssignableFrom<Patient>(okResult.Value);
            Assert.NotNull(patient);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsBadRequest()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Get(0);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetById_WithNonExistentId_ReturnsNotFoundResult()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Get(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

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
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

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
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

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
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

            var controller = new PatientsController(mockLogger.Object, mockService);

            var newEntry = new Patient()
            {
                Id = 3,
                FirstName = "Jonny",
                LastName = "Luck"
            };

            var result = await controller.Put(3, newEntry);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistent_ReturnsNotFound()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Delete(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeletesOneAndReturnsNoContent()
        {
            var people = await GetTestPatients();
            IPatientRepository mockRepository = new PatientRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<PatientsController>>();

            IPatientService mockService = new PatientService(mockRepository);

            var controller = new PatientsController(mockLogger.Object, mockService);

            var result = await controller.Delete(4);

            Assert.IsType<NoContentResult>(result);
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
