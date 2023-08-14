﻿using HospitalCase.Domain.Interfaces;
using HospitalCase.Domain.Models;
using System.Text.RegularExpressions;

namespace HospitalCase.Domain.Validators
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

        /// <summary>
        /// Validates the CPF using regex
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns>True if the CPF is valid</returns>
        private bool IsValidCPF(string cpf)
        {
            var regex = new Regex(@"\d{3}\.\d{3}\.\d{3}-\d{2}");
            return regex.IsMatch(cpf);
        }

        /// <summary>
        /// Validates the phone number using regex and the e.164 format
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>True if the phone number is valid</returns>
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^\+?[1-9]\d{1,14}$");
            return regex.IsMatch(phoneNumber);
        }
    }
}