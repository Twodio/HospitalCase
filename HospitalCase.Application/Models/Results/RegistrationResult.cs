using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalCase.Application.Models.Results
{
    /// <summary>
    /// Result to be returned when a registration accours
    /// </summary>
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public ICollection<string> Errors { get; set; }

        public RegistrationResult()
        {
            Errors = new List<string>();
        }
    }
}
