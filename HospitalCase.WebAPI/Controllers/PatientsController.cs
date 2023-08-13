using HospitalCase.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace HospitalCase.WebAPI.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly ICollection<Patient> _patients;

        public PatientsController(ILogger<PatientsController> logger, ICollection<Patient> patients)
        {
            _logger = logger;
            _patients = patients;
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return _patients.AsEnumerable();
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public Patient Get(int id)
        {
            return _patients.SingleOrDefault(p => p.Id == id);
        }

        // POST api/<PatientsController>
        [HttpPost]
        public void Post(Patient patient)
        {
            _patients.Add(patient);
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, Patient patient)
        {
            var foundPatient = _patients.SingleOrDefault(hp => hp.Id == id);
            if (foundPatient != null)
            {
                foundPatient.FirstName = patient.FirstName;
                foundPatient.LastName = patient.LastName;
                foundPatient.BirthDate = patient.BirthDate;
                foundPatient.PhoneNumber = patient.PhoneNumber;
            }
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var foundPatient = _patients.SingleOrDefault(p => p.Id == id);
            if (foundPatient != null)
            {
                _patients.Remove(foundPatient);
            }
        }
    }
}
