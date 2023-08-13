using HospitalCase.WebAPI.Models;
using System;
using Xunit;

namespace HospitalCase.Tests.Models
{
    public class PatientTests
    {
        [Fact]
        public void Patient_With_Parameterless_Contructor_Works()
        {
            var firstName = "Jon";
            var lastName = "Doe";

            var patient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
            };

            Assert.Equal(firstName, patient.FirstName);
            Assert.Equal(lastName, patient.LastName);
            Assert.NotNull(patient.MedicalRecords);
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