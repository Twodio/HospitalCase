﻿using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using System.Collections.Generic;

namespace HospitalCase.Insfrastructure.Repositories
{
    public class PatientRepository : PersonRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalCaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
