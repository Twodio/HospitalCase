using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Controllers
{
    [Route("api/medical-records")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly ILogger<MedicalRecordsController> _logger;
        private readonly IMedicalRecordRepository _medicalRecordsRepository;

        public MedicalRecordsController(ILogger<MedicalRecordsController> logger, IMedicalRecordRepository medicalRecordsRepository)
        {
            _logger = logger;
            _medicalRecordsRepository = medicalRecordsRepository;
        }

        // GET: api/<MedicalRecordsController>
        [HttpGet]
        public async Task<IEnumerable<MedicalRecord>> Get()
        {
            var result = await _medicalRecordsRepository.GetAllAsync();
            return result;
        }

        // GET api/<MedicalRecordsController>/5
        [HttpGet("{id}")]
        public async Task<MedicalRecord> Get(int id)
        {
            var result = await _medicalRecordsRepository.GetByIdAsync(id);

            return result;
        }

        // POST api/<MedicalRecordsController>
        [HttpPost]
        public async void Post([FromBody] MedicalRecord medicalRecord)
        {
            await _medicalRecordsRepository.CreateAsync(medicalRecord);
        }

        // PUT api/<MedicalRecordsController>/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] MedicalRecord medicalRecord)
        {
            await _medicalRecordsRepository.UpdateAsync(id, medicalRecord);
        }

        // DELETE api/<MedicalRecordsController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await _medicalRecordsRepository.DeleteAsync(id);
        }
    }
}
