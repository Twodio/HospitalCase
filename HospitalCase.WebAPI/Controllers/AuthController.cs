using HospitalCase.Application.Interfaces;
using HospitalCase.Application.Models;
using HospitalCase.Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HospitalCase.WebAPI.Controllers
{
    /// <summary>
    /// Manages operations related to account authentication
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<MedicalRecordsController> _logger;

        public AuthController(ILogger<MedicalRecordsController> logger, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates an user by username and password
        /// </summary>
        /// <param name="request">The request model containing the authentication credentials</param>
        /// <returns>A result object containing the authentication success status, token, and any relevant messages</returns>
        /// <response code="200">Returns the response with the generated token</response>
        /// <response code="400">If the request is mallformed</response>
        /// <response code="500">If there is a server error</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _authenticationService.LoginAsync(request);

                if (result.Success)
                {
                    return Ok(result);
                }

                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
