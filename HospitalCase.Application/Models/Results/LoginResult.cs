using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalCase.Application.Models.Results
{
    /// <summary>
    /// Result to be returned when login accours
    /// </summary>
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public LoginResult()
        {
            Errors = new List<string>();
        }
    }
}
