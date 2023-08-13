using HospitalCase.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalCase.Tests.Models
{
    public class DoctorTests
    {
        [Fact]
        public void Doctor_With_Parameterless_Contructor_Works()
        {
            var firstName = "Jon";
            var lastName = "Doe";
            var type = HealthcareProviderType.Doctor;

            var doctor = new HealthcareProvider()
            {
                FirstName = firstName,
                LastName = lastName,
                Type = type
            };

            Assert.Equal(firstName, doctor.FirstName);
            Assert.Equal(lastName, doctor.LastName);
            Assert.Equal(type, doctor.Type);
        }
    }
}
