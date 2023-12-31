﻿using System.Collections.Generic;

namespace HospitalCase.Domain.Models
{
    public class Patient : Person
    {
        public ICollection<MedicalRecord> MedicalRecords { get; set; }

        public Patient()
        {
            MedicalRecords = new HashSet<MedicalRecord>();
        }
    }
}
