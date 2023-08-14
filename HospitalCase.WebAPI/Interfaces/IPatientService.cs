using HospitalCase.WebAPI.Models;

namespace HospitalCase.WebAPI.Interfaces
{
    public interface IPatientService : IPersonService<int, Patient>
    {

    }
}
