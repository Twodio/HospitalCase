using HospitalCase.WebAPI.Interfaces;
using HospitalCase.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HospitalCase.WebAPI.Validators
{
    public class PersonValidator : IValidator<Person>
    {
        public ValidationResult Validate(Person entity)
        {
            var result = new ValidationResult();

            if(string.IsNullOrWhiteSpace(entity.FirstName) || entity.FirstName.Length > 50)
            {
                result.Errors.Add("First name is required ans must be less than 50 characters.");
            }

            if (string.IsNullOrWhiteSpace(entity.LastName) || entity.LastName.Length > 50)
            {
                result.Errors.Add("Last name is required ans must be less than 50 characters.");
            }

            if (string.IsNullOrWhiteSpace(entity.CPF) || !IsValidCPF(entity.CPF))
            {
                result.Errors.Add("CPF is required and must be valid.");
            }

            if (string.IsNullOrWhiteSpace(entity.PhoneNumber) || !IsValidPhoneNumber(entity.PhoneNumber))
            {
                result.Errors.Add("Phone number is required and must be valid.");
            }

            return result;
        }

        private bool IsValidCPF(string cpf)
        {
            var regex = new Regex(@"\d{3}\.\d{3}\.\d{3}-\d{2}");
            return regex.IsMatch(cpf);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^\+?[1-9]\d{1,14}$");
            return regex.IsMatch(phoneNumber);
        }
    }
}
