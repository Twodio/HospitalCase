using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Interfaces
{
    public interface IHealthcareProviderService : IPersonService<int, HealthcareProvider>
    {
    }
}
