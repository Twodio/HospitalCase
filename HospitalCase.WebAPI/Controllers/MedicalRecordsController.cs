using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _medicalRecordsRepository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<MedicalRecordsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();

            try
            {
                var foundEntry = await _medicalRecordsRepository.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                return Ok(foundEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<MedicalRecordsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MedicalRecord medicalRecord)
        {
            try
            {
                await _medicalRecordsRepository.CreateAsync(medicalRecord);
                return CreatedAtAction(nameof(Get), new { id = medicalRecord.Id }, medicalRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<MedicalRecordsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Id) return BadRequest();

            try
            {
                var foundEntry = await _medicalRecordsRepository.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _medicalRecordsRepository.UpdateAsync(id, medicalRecord);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<MedicalRecordsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var foundEntry = await _medicalRecordsRepository.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _medicalRecordsRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
