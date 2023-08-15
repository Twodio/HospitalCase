using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalCase.Domain.Exceptions
{
    public class InvalidCPFException : ApplicationException
    {
        public string CPF { get; set; }

        public InvalidCPFException() { }

        public InvalidCPFException(string message, string cPF) : base(message)
        {
            CPF = cPF;
        }
    }
}
