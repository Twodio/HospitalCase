using HospitalCase.Domain.Models;
using System;

namespace HospitalCase.Application.Common
{
    public static class UserRoles
    {
        public const string Patient = "Doctor";
        public const string Doctor = "Doctor";
        public const string Nurse = "Nurse";

        /// <summary>
        /// Returns the string equivalent role based on the healthcare provider type
        /// </summary>
        /// <param name="type">The healthcare type to return</param>
        /// <returns>A string based type of the healthcare provider</returns>
        /// <exception cref="ArgumentException">If none is found</exception>
        public static string FromType(HealthcareProviderType type)
        {
            switch (type)
            {
                case HealthcareProviderType.Doctor:
                    return Doctor;
                case HealthcareProviderType.Nurse:
                    return Nurse;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
