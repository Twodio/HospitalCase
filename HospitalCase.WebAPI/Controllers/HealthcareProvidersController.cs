using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Controllers
{
    [Route("api/healthcare-providers")]
    [ApiController]
    public class HealthcareProvidersController : ControllerBase
    {
        private readonly ILogger<HealthcareProvidersController> _logger;
        private readonly IHealthcareProviderRepository _healthcareProvidersRepository;

        public HealthcareProvidersController(ILogger<HealthcareProvidersController> logger, IHealthcareProviderRepository healthcareProvidersRepository)
        {
            _logger = logger;
            _healthcareProvidersRepository = healthcareProvidersRepository;
        }

        // GET: api/<HealthcareProvidersControllers>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _healthcareProvidersRepository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<HealthcareProvidersControllers>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();

            try
            {
                var foundEntry = await _healthcareProvidersRepository.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                return Ok(foundEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<HealthcareProvidersControllers>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HealthcareProvider healthcareProvider)
        {
            try
            {
                await _healthcareProvidersRepository.CreateAsync(healthcareProvider);
                return CreatedAtAction(nameof(Get), new { id = healthcareProvider.Id }, healthcareProvider);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<HealthcareProvidersControllers>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] HealthcareProvider healthcareProvider)
        {
            if (id != healthcareProvider.Id) return BadRequest();

            try
            {
                var foundEntry = await _healthcareProvidersRepository.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _healthcareProvidersRepository.UpdateAsync(id, healthcareProvider);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<HealthcareProvidersControllers>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var foundEntry = await _healthcareProvidersRepository.GetByIdAsync(id);

                if (foundEntry == null) return NotFound();

                await _healthcareProvidersRepository.DeleteAsync(id);

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
