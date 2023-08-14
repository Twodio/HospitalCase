using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Interfaces
{
    public interface IPatientRepository : IPersonRepository<Patient>
    {
    }
}
