namespace HospitalCase.Application.Models.Requests
{
    /// <summary>
    /// Base model for user login
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
