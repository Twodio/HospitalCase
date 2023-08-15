using HospitalCase.Application.Interfaces;
using HospitalCase.Application.Models.Requests;
using HospitalCase.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Controllers
{
    /// <summary>
    /// Manages operations related to account creation
    /// </summary>
    [Route("api/register")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<MedicalRecordsController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public RegistrationController(ILogger<MedicalRecordsController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Creates a new user associated with a patient
        /// </summary>
        /// <param name="request">The registration details for the patient</param>
        /// <returns>A result object containing the success status and any relevant messages</returns>
        /// <response code="201">Returns the created patient response with the ID</response>
        /// <response code="400">If the request is mallformed</response>
        /// <response code="500">If there is a server error</response>
        [HttpPost]
        [Route("patient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterPatient(PatientRegistrationRequest request)
        {
            try
            {
                var result = await _authenticationService.RegisterPatientAsync(request);

                if (result.Success)
                {
                    return CreatedAtAction(nameof(RegisterPatient), new { id = result.UserId }, result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates a new user associated with a healthcare provider
        /// </summary>
        /// <param name="request">The registration details for the healthcare provider</param>
        /// <returns>A result object containing the success status and any relevant messages</returns>
        /// <response code="201">Returns the created healthcare provider response with the ID</response>
        /// <response code="400">If the request is mallformed</response>
        /// <response code="500">If there is a server error</response>
        [HttpPost]
        [Route("healthcare-provider")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterHealthcareProvider(HealthcareProviderRegistrationRequest request)
        {
            try
            {
                var result = await _authenticationService.RegisterHealthcareProviderAsync(request);

                if (result.Success)
                {
                    return CreatedAtAction(nameof(RegisterPatient), new { id = result.UserId }, result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
