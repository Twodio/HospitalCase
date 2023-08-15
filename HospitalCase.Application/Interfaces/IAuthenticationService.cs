using HospitalCase.Application.Models;
using HospitalCase.Application.Models.Requests;
using HospitalCase.Application.Models.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCase.Application.Interfaces
{
    /// <summary>
    /// Provides methods for user authentication, including registration and login
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Registers a new user associated with a Patient
        /// </summary>
        /// <param name="request">The registration details for the patient</param>
        /// <returns>A result object containing the success status and any relevant messages</returns>
        Task<RegistrationResult> RegisterPatientAsync(PatientRegistrationRequest request);

        /// <summary>
        /// Registers a new user associated with an healthcare provider
        /// </summary>
        /// <param name="request">The registration details for the healthcare provider</param>
        /// <returns>A result object containing the success status and any relevant messages</returns>
        Task<RegistrationResult> RegisterHealthcareProviderAsync(HealthcareProviderRegistrationRequest request);

        /// <summary>
        /// Authenticates an user by username and password
        /// </summary>
        /// <param name="request">The request model containing the authentication credentials</param>
        /// <returns>A result object containing the authentication success status, token, and any relevant messages</returns>
        Task<LoginResult> LoginAsync(LoginRequest request);
    }
}
