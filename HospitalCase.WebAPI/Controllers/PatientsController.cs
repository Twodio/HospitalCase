using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientRepository _patientsRepository;

        public PatientsController(ILogger<PatientsController> logger, IPatientRepository patientsRepository)
        {
            _logger = logger;
            _patientsRepository = patientsRepository;
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public async Task<IEnumerable<Patient>> Get()
        {
            var result = await _patientsRepository.GetAllAsync();

            return result;
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public async Task<Patient> Get(int id)
        {
            var result = await _patientsRepository.GetByIdAsync(id);

            return result;
        }

        // POST api/<PatientsController>
        [HttpPost]
        public async void Post(Patient patient)
        {
            await _patientsRepository.CreateAsync(patient);
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public async void Put(int id, Patient patient)
        {
            await _patientsRepository.UpdateAsync(id, patient);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await _patientsRepository.DeleteAsync(id);
        }
    }
}
