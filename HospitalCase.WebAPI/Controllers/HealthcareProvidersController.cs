using HospitalCase.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HospitalCase.WebAPI.Controllers
{
    [Route("api/healthcare-providers")]
    [ApiController]
    public class HealthcareProvidersController : ControllerBase
    {
        private readonly ILogger<HealthcareProvidersController> _logger;
        private readonly ICollection<HealthcareProvider> _healthcareProviders;

        public HealthcareProvidersController(ILogger<HealthcareProvidersController> logger, ICollection<HealthcareProvider> healthcareProviders)
        {
            _logger = logger;
            _healthcareProviders = healthcareProviders;
        }

        // GET: api/<HealthcareProvidersControllers>
        [HttpGet]
        public IEnumerable<HealthcareProvider> Get()
        {
            return _healthcareProviders.AsEnumerable();
        }

        // GET api/<HealthcareProvidersControllers>/5
        [HttpGet("{id}")]
        public HealthcareProvider Get(int id)
        {
            return _healthcareProviders.SingleOrDefault(hp => hp.Id == id);
        }

        // POST api/<HealthcareProvidersControllers>
        [HttpPost]
        public void Post([FromBody] HealthcareProvider healthcareProvider)
        {
            _healthcareProviders.Add(healthcareProvider);
        }

        // PUT api/<HealthcareProvidersControllers>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] HealthcareProvider healthcareProvider)
        {
            var foundHealthcareProvider = _healthcareProviders.SingleOrDefault(hp => hp.Id == id);
            if (foundHealthcareProvider != null)
            {
                foundHealthcareProvider.FirstName = healthcareProvider.FirstName;
                foundHealthcareProvider.LastName = healthcareProvider.LastName;
                foundHealthcareProvider.BirthDate = healthcareProvider.BirthDate;
                foundHealthcareProvider.PhoneNumber = healthcareProvider.PhoneNumber;
                foundHealthcareProvider.Type = healthcareProvider.Type;
            }
        }

        // DELETE api/<HealthcareProvidersControllers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var foundHealthcareProvider = _healthcareProviders.SingleOrDefault(hp => hp.Id == id);
            if (foundHealthcareProvider != null)
            {
                _healthcareProviders.Remove(foundHealthcareProvider);
            }
        }
    }
}
