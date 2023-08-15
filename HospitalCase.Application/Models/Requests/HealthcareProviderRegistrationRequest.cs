using HospitalCase.Domain.Models;

namespace HospitalCase.Application.Models.Requests
{
    /// <summary>
    /// Model for the healthcare provider registration
    /// </summary>
    public class HealthcareProviderRegistrationRequest : RegistrationRequest
    {
        public HealthcareProviderType Type { get; set; }
    }
}
