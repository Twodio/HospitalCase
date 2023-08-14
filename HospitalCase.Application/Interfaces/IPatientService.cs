using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Interfaces
{
    public interface IPatientService : IPersonService<int, Patient>
    {

    }
}
