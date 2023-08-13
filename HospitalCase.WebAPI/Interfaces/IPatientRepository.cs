using HospitalCase.WebAPI.Models;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IPatientRepository : IPersonRepository<Patient>
    {
    }
}
