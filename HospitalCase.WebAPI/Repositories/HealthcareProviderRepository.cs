using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using System.Collections.Generic;

namespace HospitalCase.WebAPI.Repositories
{
    public class HealthcareProviderRepository : PersonRepository<HealthcareProvider>, IHealthcareProviderRepository
    {
        public HealthcareProviderRepository(ICollection<HealthcareProvider> people) : base(people)
        {
        }
    }
}
