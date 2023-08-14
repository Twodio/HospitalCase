using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;

namespace HospitalCase.WebAPI.Services
{
    public class PatientService : PersonService<Patient>, IPatientService
    {
        public PatientService(IPersonRepository<Patient> personRepository) : base(personRepository)
        {
        }
    }
}
