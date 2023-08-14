using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Controllers
{
    /// <summary>
    /// Manages operations related to the patients
    /// </summary>
    [Authorize]
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _patientsService;

        public PatientsController(ILogger<PatientsController> logger, IPatientService patientsService)
        {
            _logger = logger;
            _patientsService = patientsService;
        }

        /// <summary>
        /// Retrieves all the patients
        /// </summary>
        /// <returns>Detailed information of the patients</returns>
        /// <response code="200">Returns the patients list</response>
        /// <response code="500">If there is a server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _patientsService.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a patient by ID
        /// </summary>
        /// <param name="id">The ID of the patient</param>
        /// <returns>The patient details</returns>
        /// <response code="200">Returns the patient details</response>
        /// <response code="404">If the patient is not found</response>
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
                var foundEntry = await _patientsService.GetByIdAsync(id);

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
        /// Creates a patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>The created patient</returns>
        /// <response code="201">Returns the created patientr</response>
        /// <response code="500">If there is a server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Patient patient)
        {
            try
            {
                await _patientsService.CreateAsync(patient);
                return CreatedAtAction(nameof(Get), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates a patient
        /// </summary>
        /// <param name="id">The ID of the patient</param>
        /// <param name="patient">The updated patient</param>
        /// <response code="404">If the patient is not found</response>
        /// <response code="204">If the patient was updated</response>
        /// <response code="400">If the request is mallformed</response>
        /// <response code="500">If there is a server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, Patient patient)
        {
            if (id != patient.Id) return BadRequest();

            try
            {
                var foundEntry = await _patientsService.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _patientsService.UpdateAsync(id, patient);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a patient
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">If the patient is not found</response>
        /// <response code="204">If the patient was deleted</response>
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
                var foundEntry = await _patientsService.GetByIdAsync(id);

                if(foundEntry == null) return NotFound();

                await _patientsService.DeleteAsync(id);
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
