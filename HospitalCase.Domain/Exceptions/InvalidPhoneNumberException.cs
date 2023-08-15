using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalCase.Domain.Exceptions
{
    public class InvalidPhoneNumberException : ApplicationException
    {
        public string PhoneNumber { get; set; }

        public InvalidPhoneNumberException() { }

        public InvalidPhoneNumberException(string message, string phoneNumber) : base(message)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
