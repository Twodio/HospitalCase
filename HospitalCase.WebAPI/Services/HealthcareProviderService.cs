using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;

namespace HospitalCase.WebAPI.Services
{
    public class HealthcareProviderService : PersonService<HealthcareProvider>, IHealthcareProviderService
    {
        public HealthcareProviderService(IPersonRepository<HealthcareProvider> personRepository) : base(personRepository)
        {
        }
    }
}
