using HospitalCase.WebAPI.Models;
using HospitalCase.WebAPI.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalCase.Tests.Models
{
    public class HealthcareProviderTests
    {
        [Fact]
        public void HealthcareProvider_With_ValidFields_ReturnsNoErrors()
        {
            var firstName = "Jon";
            var lastName = "Doe";
            var type = HealthcareProviderType.Doctor;

            var doctor = new HealthcareProvider()
            {
                FirstName = firstName,
                LastName = lastName,
                Type = type,
                CPF = "000.000.000-00",
                PhoneNumber = "+1234567890"
            };

            var mockValidator = new Mock<PersonValidator>();
            var validation = mockValidator.Object.Validate(doctor);

            Assert.True(validation.IsValid);
            Assert.Empty(validation.Errors);
            Assert.Equal(firstName, doctor.FirstName);
            Assert.Equal(lastName, doctor.LastName);
            Assert.Equal(type, doctor.Type);
        }

        [Fact]
        public void HealthcareProvider_With_InvalidCPF_ReturnsError()
        {
            var firstName = "Jon";
            var lastName = "Doe";
            var type = HealthcareProviderType.Doctor;

            var doctor = new HealthcareProvider()
            {
                FirstName = firstName,
                LastName = lastName,
                Type = type,
                CPF = "000.000.000-0",
                PhoneNumber = "+1234567890"
            };

            var mockValidator = new Mock<PersonValidator>();
            var validation = mockValidator.Object.Validate(doctor);

            Assert.False(validation.IsValid);
            Assert.Single(validation.Errors);
            Assert.Equal(firstName, doctor.FirstName);
            Assert.Equal(lastName, doctor.LastName);
            Assert.Equal(type, doctor.Type);
        }

        [Fact]
        public void HealthcareProvider_With_InvalidPhoneNumber_ReturnsError()
        {
            var firstName = "Jon";
            var lastName = "Doe";
            var type = HealthcareProviderType.Doctor;

            var doctor = new HealthcareProvider()
            {
                FirstName = firstName,
                LastName = lastName,
                Type = type,
                CPF = "000.000.000-00",
                PhoneNumber = "01234567890"
            };

            var mockValidator = new Mock<PersonValidator>();
            var validation = mockValidator.Object.Validate(doctor);

            Assert.False(validation.IsValid);
            Assert.Single(validation.Errors);
            Assert.Equal(firstName, doctor.FirstName);
            Assert.Equal(lastName, doctor.LastName);
            Assert.Equal(type, doctor.Type);
        }
    }
}
