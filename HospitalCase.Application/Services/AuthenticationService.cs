using HospitalCase.Application.Common;
using HospitalCase.Application.Interfaces;
using HospitalCase.Application.Models;
using HospitalCase.Application.Models.Requests;
using HospitalCase.Application.Models.Results;
using HospitalCase.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCase.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IPatientService _patientService;
        private readonly IHealthcareProviderService _healthcareProviderService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, ITokenService tokenService, IPatientService patientService, IHealthcareProviderService healthcareProviderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _patientService = patientService;
            _healthcareProviderService = healthcareProviderService;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            // Validate

            // Signin
            var signInResult = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (!signInResult.Succeeded)
            {
                // TODO: Handle other reason

                return new LoginResult()
                {
                    Success = false
                };
            }

            var user = await _userManager.FindByNameAsync(request.Username);

            // Create the Jwt if the credentials are valid
            var jwtToken = await _tokenService.CreateToken(user);

            var result = new LoginResult()
            {
                Success = true,
                Token = jwtToken,
                Message = "Success!"
            };

            return result;
        }

        public async Task<RegistrationResult> RegisterPatientAsync(PatientRegistrationRequest request)
        {
            // Validate

            await _unitOfWork.BeginTransationAsync();
            try
            {
                var patient = new Patient()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CPF = request.CPF,
                    PhoneNumber = request.PhoneNumber,
                };

                var createdPatient = await _patientService.CreateAsync(patient);

                // Create the user (defaults to patient)
                var user = new ApplicationUser()
                {
                    UserName = request.Username,
                    Person = createdPatient
                };

                // TODO: Can be refactored

                // Save the user
                var signUpResult = await _userManager.CreateAsync(user, request.Password);

                // Add to a role
                await _userManager.AddToRoleAsync(user, UserRoles.Patient);

                var result = new RegistrationResult();

                // If there is any error
                if (signUpResult.Errors?.Count() > 0)
                {
                    foreach (var error in signUpResult.Errors)
                    {
                        result.Errors.Add(error.Description);
                    }

                    result.Success = false;

                    return result;
                }

                result.Success = true;
                result.Message = "Success";
                result.UserId = user.Id;

                await _unitOfWork.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<RegistrationResult> RegisterHealthcareProviderAsync(HealthcareProviderRegistrationRequest request)
        {
            // Validate

            await _unitOfWork.BeginTransationAsync();
            try
            {
                var healthcareProvider = new HealthcareProvider()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CPF = request.CPF,
                    PhoneNumber = request.PhoneNumber,
                    Type = request.Type
                };

                var createdHealthcareProvider = await _healthcareProviderService.CreateAsync(healthcareProvider);

                // Create the user (defaults to patient)
                var user = new ApplicationUser()
                {
                    UserName = request.Username,
                    Person = createdHealthcareProvider
                };

                // TODO: Can be refactored

                // Save the user
                var signUpResult = await _userManager.CreateAsync(user, request.Password);

                // Add to a role
                await _userManager.AddToRoleAsync(user, UserRoles.FromType(request.Type));

                var result = new RegistrationResult();

                // If there is any error
                if (signUpResult.Errors?.Count() > 0)
                {
                    foreach (var error in signUpResult.Errors)
                    {
                        result.Errors.Add(error.Description);
                    }

                    result.Success = false;

                    return result;
                }

                result.Success = true;
                result.Message = "Success";
                result.UserId = user.Id;

                await _unitOfWork.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
