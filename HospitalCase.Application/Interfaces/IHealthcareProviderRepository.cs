using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Interfaces
{
    public interface IHealthcareProviderRepository : IPersonRepository<HealthcareProvider>
    {
    }
}
