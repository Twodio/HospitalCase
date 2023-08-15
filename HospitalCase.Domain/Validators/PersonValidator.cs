using HospitalCase.Domain.Exceptions;
using HospitalCase.Domain.Interfaces;
using HospitalCase.Domain.Models;
using System.Text.RegularExpressions;

namespace HospitalCase.Domain.Validators
{
    public class PersonValidator<TEntity> : IValidator<TEntity> where TEntity : Person, new()
    {
        public void Validate(TEntity entity)
        {
            if(string.IsNullOrWhiteSpace(entity.FirstName) || entity.FirstName.Length > 50)
            {
                throw new FirstAndLastNameAreRequiredException("First name is required ans must be less than 50 characters.", entity.FirstName, entity.LastName);
            }

            if (string.IsNullOrWhiteSpace(entity.LastName) || entity.LastName.Length > 50)
            {
                throw new FirstAndLastNameAreRequiredException("First name is required ans must be less than 50 characters.", entity.FirstName, entity.LastName);
            }

            if (string.IsNullOrWhiteSpace(entity.CPF) || !IsValidCPF(entity.CPF))
            {
                throw new InvalidCPFException("CPF is required and must be valid.", entity.CPF);
            }

            if (string.IsNullOrWhiteSpace(entity.PhoneNumber) || !IsValidPhoneNumber(entity.PhoneNumber))
            {
                throw new InvalidPhoneNumberException("Phone number is required and must be valid.", entity.PhoneNumber);
            }
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
