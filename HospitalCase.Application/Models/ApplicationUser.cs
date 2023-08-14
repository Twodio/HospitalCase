using HospitalCase.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalCase.Application.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
