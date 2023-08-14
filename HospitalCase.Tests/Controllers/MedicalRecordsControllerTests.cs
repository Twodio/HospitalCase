using HospitalCase.Application.Interfaces;
using HospitalCase.Application.Services;
using HospitalCase.Domain.Models;
using HospitalCase.Insfrastructure.Repositories;
using HospitalCase.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HospitalCase.Tests.Controllers
{
    public class MedicalRecordsControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResult_WithMedicalRecords()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var medicalRecords = Assert.IsAssignableFrom<IEnumerable<MedicalRecord>>(okResult.Value);
            Assert.Equal(2, medicalRecords.Count());
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult_WithOneMedicalRecord()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

            var result = await controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var medicalRecord = Assert.IsAssignableFrom<MedicalRecord>(okResult.Value);
            Assert.NotNull(medicalRecord);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsBadRequest()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

            var result = await controller.Get(0);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetById_WithNonExistentId_ReturnsNotFoundResult()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

            var result = await controller.Get(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

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

            var result = await controller.Post(newEntry);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var patient = Assert.IsAssignableFrom<MedicalRecord>(createdAtActionResult.Value);
            Assert.NotNull(patient);
        }

        [Fact]
        public async Task Put_MissmatchedId_ReturnsBadRequest()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

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

            var result = await controller.Put(2, newEntry);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Put_NonExistent_ReturnsNotFound()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

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

            var result = await controller.Put(3, newEntry);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Put_UpdatesOneAndReturnsNoContent()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

            var newEntry = new MedicalRecord()
            {
                Id = 1,
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

            var result = await controller.Put(1, newEntry);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistent_ReturnsNotFound()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

            var result = await controller.Delete(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeletesOneAndReturnsNoContent()
        {
            var people = await GetTestMedicalRecords();
            IMedicalRecordRepository mockRepository = new MedicalRecordRepository(people.ToHashSet());
            var mockLogger = new Mock<ILogger<MedicalRecordsController>>();

            IMedicalRecordService mockService = new MedicalRecordService(mockRepository);

            var controller = new MedicalRecordsController(mockLogger.Object, mockService);

            var result = await controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
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
