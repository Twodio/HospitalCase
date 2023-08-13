using HospitalCase.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalCase.Tests.Models
{
    public class MedicalRecordTests
    {
        [Fact]
        public void MedicalRecord_PatientAndHealthcareProvider_RelationshipWorks()
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

            // Assert Patient
            Assert.NotNull(medicalRecord.Patient);
            Assert.Equal(patientFirstName, medicalRecord.Patient.FirstName);
            Assert.Equal(patientLastName, medicalRecord.Patient.LastName);

            // Assert Doctor
            Assert.NotNull(medicalRecord.HealthcareProvider);
            Assert.Equal(doctorFirstName, medicalRecord.HealthcareProvider.FirstName);
            Assert.Equal(doctorLastName, medicalRecord.HealthcareProvider.LastName);

            // Assert Other
            Assert.Equal(medicalRecordDate, medicalRecord.RecordDate);
            Assert.Equal(medicalRecordDiagnosis, medicalRecordDiagnosis);
        }
    }
}
