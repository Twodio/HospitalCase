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
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

            // Create the user (defaults to patient)
            var user = new ApplicationUser()
            {
                UserName = request.Username,
                Person = new Patient()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CPF = request.CPF,
                    PhoneNumber = request.PhoneNumber,
                }
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

            return result;
        }

        public async Task<RegistrationResult> RegisterHealthcareProviderAsync(HealthcareProviderRegistrationRequest request)
        {
            // Validate

            // Create the user (defaults to patient)
            var user = new ApplicationUser()
            {
                UserName = request.Username,
                Person = new HealthcareProvider()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CPF = request.CPF,
                    PhoneNumber = request.PhoneNumber,
                    Type = request.Type
                }
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

            return result;
        }
    }
}
