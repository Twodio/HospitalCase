using HospitalCase.WebAPI.Models;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IHealthcareProviderService : IPersonService<int, HealthcareProvider>
    {
    }
}
