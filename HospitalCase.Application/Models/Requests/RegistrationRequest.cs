namespace HospitalCase.Application.Models.Requests
{
    /// <summary>
    /// Base request model for user regristration
    /// </summary>
    public abstract class RegistrationRequest : LoginRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
