using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Interfaces;
using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Services
{
    public class PatientService : PersonService<Patient>, IPatientService
    {
        public PatientService(IPersonRepository<Patient> personRepository, IValidator<Patient> validator) : base(personRepository, validator)
        {
        }
    }
}
