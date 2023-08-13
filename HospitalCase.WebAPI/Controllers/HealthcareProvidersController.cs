using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<IEnumerable<HealthcareProvider>> Get()
        {
            var result = await _healthcareProvidersRepository.GetAllAsync();

            return result;
        }

        // GET api/<HealthcareProvidersControllers>/5
        [HttpGet("{id}")]
        public async Task<HealthcareProvider> Get(int id)
        {
            var foundEntry = await _healthcareProvidersRepository.GetByIdAsync(id);

            return foundEntry;
        }

        // POST api/<HealthcareProvidersControllers>
        [HttpPost]
        public async void Post([FromBody] HealthcareProvider healthcareProvider)
        {
            await _healthcareProvidersRepository.CreateAsync(healthcareProvider);
        }

        // PUT api/<HealthcareProvidersControllers>/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] HealthcareProvider healthcareProvider)
        {
            await _healthcareProvidersRepository.UpdateAsync(id, healthcareProvider);
        }

        // DELETE api/<HealthcareProvidersControllers>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await _healthcareProvidersRepository.DeleteAsync(id);
        }
    }
}
