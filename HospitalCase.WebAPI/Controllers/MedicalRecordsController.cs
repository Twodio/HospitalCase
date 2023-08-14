using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Controllers
{
    /// <summary>
    /// Manages operations related to the medical records
    /// </summary>
    [Route("api/medical-records")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly ILogger<MedicalRecordsController> _logger;
        private readonly IMedicalRecordService _medicalRecordsService;

        public MedicalRecordsController(ILogger<MedicalRecordsController> logger, IMedicalRecordService medicalRecordsService)
        {
            _logger = logger;
            _medicalRecordsService = medicalRecordsService;
        }

        /// <summary>
        /// Retrieves all the medical records
        /// </summary>
        /// <returns>Detailed information of the medical records</returns>
        /// <response code="200">Returns the medical records list</response>
        /// <response code="500">If there is a server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _medicalRecordsService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a medical record by ID
        /// </summary>
        /// <param name="id">The ID of the medical record</param>
        /// <returns>The medical record details</returns>
        /// <response code="200">Returns the medical record details</response>
        /// <response code="404">If the medical record is not found</response>
        /// <response code="500">If there is a server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();

            try
            {
                var foundEntry = await _medicalRecordsService.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                return Ok(foundEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates a medical record
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns>The created medical record</returns>
        /// <response code="201">Returns the created medical record</response>
        /// <response code="500">If there is a server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] MedicalRecord medicalRecord)
        {
            try
            {
                await _medicalRecordsService.CreateAsync(medicalRecord);
                return CreatedAtAction(nameof(Get), new { id = medicalRecord.Id }, medicalRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates a medical record
        /// </summary>
        /// <param name="id">The ID of the medical record</param>
        /// <param name="medicalRecord">The updated medical record</param>
        /// <response code="404">If the medical record is not found</response>
        /// <response code="204">If the medical record was updated</response>
        /// <response code="400">If the request is mallformed</response>
        /// <response code="500">If there is a server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Id) return BadRequest();

            try
            {
                var foundEntry = await _medicalRecordsService.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _medicalRecordsService.UpdateAsync(id, medicalRecord);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a medical record
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">If the medical record is not found</response>
        /// <response code="204">If the medical record was deleted</response>
        /// <response code="400">If the request is mallformed</response>
        /// <response code="500">If there is a server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            try
            {
                var foundEntry = await _medicalRecordsService.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _medicalRecordsService.DeleteAsync(id);

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
