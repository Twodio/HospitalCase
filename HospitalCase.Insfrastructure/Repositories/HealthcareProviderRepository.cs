using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using System.Collections.Generic;

namespace HospitalCase.Insfrastructure.Repositories
{
    public class HealthcareProviderRepository : PersonRepository<HealthcareProvider>, IHealthcareProviderRepository
    {
        public HealthcareProviderRepository(HospitalCaseDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
