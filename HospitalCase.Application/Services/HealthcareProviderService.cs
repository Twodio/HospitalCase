using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Interfaces;
using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Services
{
    public class HealthcareProviderService : PersonService<HealthcareProvider>, IHealthcareProviderService
    {
        public HealthcareProviderService(IPersonRepository<HealthcareProvider> personRepository, IValidator<HealthcareProvider> validator) : base(personRepository, validator)
        {
        }
    }
}
