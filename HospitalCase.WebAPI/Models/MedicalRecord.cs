﻿using System;

namespace HospitalCase.WebAPI.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public HealthcareProvider HealthcareProvider { get; set; }
        public DateTime RecordDate { get; set; }
        public string Diagnosis { get; set; }   
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
        public string Notes { get; set; }
    }
}