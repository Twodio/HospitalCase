using HospitalCase.Domain.Models;
using HospitalCase.Domain.Validators;
using Moq;
using System;
using Xunit;

namespace HospitalCase.Tests.Models
{
    public class PatientTests
    {
        [Fact]
        public void Patient_With_ValidFields_ReturnsNoErrors()
        {
            var firstName = "Jon";
            var lastName = "Doe";

            var patient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                CPF = "000.000.000-00",
                PhoneNumber = "+1234567890"
            };

            var mockValidator = new Mock<PersonValidator>();
            var validation = mockValidator.Object.Validate(patient);

            Assert.True(validation.IsValid);
            Assert.NotNull(patient.MedicalRecords);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Patient_With_InvalidCPF_ReturnsError()
        {
            var firstName = "Jon";
            var lastName = "Doe";

            var patient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                CPF = "000.000.000-0",
                PhoneNumber = "+1234567890"
            };

            var mockValidator = new Mock<PersonValidator>();
            var validation = mockValidator.Object.Validate(patient);

            Assert.False(validation.IsValid);
            Assert.NotNull(patient.MedicalRecords);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Patient_With_InvalidPhoneNumber_ReturnsError()
        {
            var firstName = "Jon";
            var lastName = "Doe";

            var patient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                CPF = "000.000.000-00",
                PhoneNumber = "01234567890"
            };

            var mockValidator = new Mock<PersonValidator>();
            var validation = mockValidator.Object.Validate(patient);

            Assert.False(validation.IsValid);
            Assert.NotNull(patient.MedicalRecords);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Patient_AddNewMedicalRecord_Works()
        {
            // Patient information
            var patientFirstName = "Jon";
            var patientLastName = "Doe";

            var patient = new Patient()
            {
                FirstName = patientFirstName,
                LastName = patientLastName,
            };

            // Doctor information
            var doctorFirstName = "Jane";
            var doctorLastName = "Smith";

            var doctor = new HealthcareProvider()
            {
                FirstName = doctorFirstName,
                LastName = doctorLastName,
                Type = HealthcareProviderType.Doctor
            };

            // Medical record information
            var medicalRecordDate = new DateTime(2023, 08, 13);
            var medicalRecordDiagnosis = "Flu";

            var medicalRecord = new MedicalRecord()
            {
                Patient = patient,
                HealthcareProvider = doctor,
                RecordDate = medicalRecordDate,
                Diagnosis = medicalRecordDiagnosis
            };

            patient.MedicalRecords.Add(medicalRecord);

            Assert.NotNull(patient.MedicalRecords);
            var patientHasOneMedicalRecord = patient.MedicalRecords.Count == 1;
            Assert.True(patientHasOneMedicalRecord);
            Assert.Contains(medicalRecord, patient.MedicalRecords);
        }
    }
}
