using System;

namespace HospitalCase.Domain.Models
{
    public class MedicalRecord : DomainObject
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int HealthcareProviderId { get; set; }
        public HealthcareProvider HealthcareProvider { get; set; }

        public DateTime RecordDate { get; set; }

        public string Diagnosis { get; set; }   

        public string Symptoms { get; set; }

        public string Treatment { get; set; }

        public string Notes { get; set; }
    }
}
