using HospitalCase.Application.Common;
using HospitalCase.Application.Interfaces;
using HospitalCase.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ClaimTypes = HospitalCase.Application.Common.ClaimTypes;

namespace HospitalCase.WebAPI.Controllers
{
    /// <summary>
    /// Manages operations related to the healthcare providers
    /// </summary>
    [Authorize]
    [Route("api/healthcare-providers")]
    [ApiController]
    public class HealthcareProvidersController : ControllerBase
    {
        private readonly ILogger<HealthcareProvidersController> _logger;
        private readonly IHealthcareProviderService _healthcareProvidersService;

        public HealthcareProvidersController(ILogger<HealthcareProvidersController> logger, IHealthcareProviderService healthcareProvidersService)
        {
            _logger = logger;
            _healthcareProvidersService = healthcareProvidersService;
        }

        /// <summary>
        /// Retrieves all the healthcare providers
        /// </summary>
        /// <returns>Detailed information of the healthcare providers</returns>
        /// <response code="200">Returns the healthcare providers list</response>
        /// <response code="500">If there is a server error</response>
        [Authorize(Policy = HealthcareProvidersPolicies.View)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _healthcareProvidersService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a healthcare provider by ID
        /// </summary>
        /// <param name="id">The ID of the healthcare provider</param>
        /// <returns>The healthcare provider details</returns>
        /// <response code="200">Returns the healthcare provider details</response>
        /// <response code="404">If the healthcare provider is not found</response>
        /// <response code="500">If there is a server error</response>
        [Authorize(Policy = HealthcareProvidersPolicies.View)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.PersonId);

            if (userId != id.ToString()) return Forbid();

            try
            {
                var foundEntry = await _healthcareProvidersService.GetByIdAsync(id);

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
        /// Creates a healthcare provider
        /// </summary>
        /// <param name="healthcareProvider"></param>
        /// <returns>The created healthcare provider</returns>
        /// <response code="201">Returns the created healthcare provider</response>
        /// <response code="500">If there is a server error</response>
        [Authorize(Policy = HealthcareProvidersPolicies.Create)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] HealthcareProvider healthcareProvider)
        {
            // TODO: BadRequest when the request has invalid first, last names or invalid phone number and cpf

            try
            {
                await _healthcareProvidersService.CreateAsync(healthcareProvider);
                return CreatedAtAction(nameof(Get), new { id = healthcareProvider.Id }, healthcareProvider);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates a healthcare provider
        /// </summary>
        /// <param name="id">The ID of the healthcare provider</param>
        /// <param name="healthcareProvider">The updated healthcare provider</param>
        /// <response code="404">If the healthcare provider is not found</response>
        /// <response code="204">If the healthcare provider was updated</response>
        /// <response code="400">If the request is mallformed</response>
        /// <response code="500">If there is a server error</response>
        [Authorize(Policy = HealthcareProvidersPolicies.Edit)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] HealthcareProvider healthcareProvider)
        {
            // TODO: BadRequest when the request has invalid first, last names or invalid phone number and cpf

            if (id != healthcareProvider.Id) return BadRequest();

            try
            {
                var foundEntry = await _healthcareProvidersService.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _healthcareProvidersService.UpdateAsync(id, healthcareProvider);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a healthcare provider
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">If the healthcare provider is not found</response>
        /// <response code="204">If the healthcare provider was deleted</response>
        /// <response code="400">If the request is mallformed</response>
        /// <response code="500">If there is a server error</response>
        [Authorize(Policy = HealthcareProvidersPolicies.Edit)]
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
                var foundEntry = await _healthcareProvidersService.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _healthcareProvidersService.DeleteAsync(id);

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
