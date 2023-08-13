using HospitalCase.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace HospitalCase.WebAPI.Controllers
{
    [Route("api/medical-records")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly ILogger<MedicalRecordsController> _logger;
        private readonly ICollection<MedicalRecord> _medicalRecords;

        public MedicalRecordsController(ILogger<MedicalRecordsController> logger, ICollection<MedicalRecord> medicalRecords)
        {
            _logger = logger;
            _medicalRecords = medicalRecords;
        }

        // GET: api/<MedicalRecordsController>
        [HttpGet]
        public IEnumerable<MedicalRecord> Get()
        {
            return _medicalRecords.AsEnumerable();
        }

        // GET api/<MedicalRecordsController>/5
        [HttpGet("{id}")]
        public MedicalRecord Get(int id)
        {
            return _medicalRecords.SingleOrDefault(mr => mr.Id == id);
        }

        // POST api/<MedicalRecordsController>
        [HttpPost]
        public void Post([FromBody] MedicalRecord medicalRecord)
        {
            _medicalRecords.Add(medicalRecord);
        }

        // PUT api/<MedicalRecordsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MedicalRecord medicalRecord)
        {
            var foundMedicalRecord = _medicalRecords.SingleOrDefault(mr => mr.Id == id);
            if (foundMedicalRecord != null)
            {
                foundMedicalRecord.Patient = medicalRecord.Patient;
                foundMedicalRecord.HealthcareProvider = medicalRecord.HealthcareProvider;
                foundMedicalRecord.RecordDate = medicalRecord.RecordDate;
                foundMedicalRecord.Symptoms = medicalRecord.Symptoms;
                foundMedicalRecord.Diagnosis = medicalRecord.Diagnosis;
                foundMedicalRecord.Treatment = medicalRecord.Treatment;
                foundMedicalRecord.Notes = medicalRecord.Notes;
            }
        }

        // DELETE api/<MedicalRecordsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var foundMedicalRecord = _medicalRecords.SingleOrDefault(mr => mr.Id == id);
            if (foundMedicalRecord != null)
            {
                _medicalRecords.Remove(foundMedicalRecord);
            }
        }
    }
}
