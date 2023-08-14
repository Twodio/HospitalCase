using System.Collections.Generic;
using System.Linq;

namespace HospitalCase.Domain.Validators
{
    /// <summary>
    /// Contains the validation states
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// List containing all the validation errors
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Returns the validation state depending of the errors count
        /// </summary>
        public bool IsValid => !Errors.Any();

        public ValidationResult()
        {
            Errors = new List<string>();
        }
    }
}
