using HospitalCase.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalCase.Application.Models
{
    /// <summary>
    /// Applicaton user associated with a person
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
