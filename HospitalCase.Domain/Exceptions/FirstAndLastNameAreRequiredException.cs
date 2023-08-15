using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalCase.Domain.Exceptions
{
    public class FirstAndLastNameAreRequiredException : ApplicationException
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public FirstAndLastNameAreRequiredException() { }

        public FirstAndLastNameAreRequiredException(string message, string firstName, string lastName) : base(message)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
