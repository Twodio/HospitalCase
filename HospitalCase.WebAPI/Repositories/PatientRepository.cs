using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Repositories
{
    public class PatientRepository : PersonRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ICollection<Patient> people) : base(people)
        {
        }
    }
}
