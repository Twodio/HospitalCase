using System;

namespace HospitalCase.Domain.Models
{
    public class MedicalRecord : DomainObject
    {
        public Patient Patient { get; set; }
        public HealthcareProvider HealthcareProvider { get; set; }
        public DateTime RecordDate { get; set; }
        public string Diagnosis { get; set; }   
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
        public string Notes { get; set; }
    }
}
