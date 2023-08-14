using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalCase.WebAPI.Models
{
    public abstract class Person : DomainObject
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string CPF { get; set; }

        public string PhotoBase64 { get; set; }
    }
}
