using System.Collections.Generic;
using System.Linq;

namespace HospitalCase.WebAPI.Validators
{
    public class ValidationResult
    {
        public List<string> Errors { get; set; }
        public bool IsValid => !Errors.Any();

        public ValidationResult()
        {
            Errors = new List<string>();
        }
    }
}
