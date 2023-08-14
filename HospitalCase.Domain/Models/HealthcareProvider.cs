namespace HospitalCase.Domain.Models
{
    public enum HealthcareProviderType
    {
        Doctor,
        Nurse,
    }

    public class HealthcareProvider : Person
    {
        public HealthcareProviderType Type { get; set; }
    }
}
