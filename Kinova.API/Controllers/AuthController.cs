using Application.Features.Accounts.Commands.Login;
using Kinova.API.Requests.Account;
using Kinova.Application.Features.Accounts.Commands.GenerateJwtToken;
using Kinova.Application.Features.Accounts.Commands.Logout;
using Kinova.Application.Features.Accounts.Commands.Register.Doctor;
using Kinova.Application.Features.Accounts.Commands.Register.Patient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kinova.API.Controllers
{
    public class AuthController : APIController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register/patient")]
        public async Task<IActionResult> Register(PatientRegisterRequest request)
        {
            var response = await _mediator.Send(new PatientRegisterCommand(request.FirstName,
                                                              request.LastName,
                                                              request.Email,
                                                              request.DateOfBirth,
                                                              request.Gender,
                                                              request.Height,
                                                              request.Weight,
                                                              request.Password,
                                                              request.ConfirmationPassword));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
        [HttpPost("register/doctor")]
        public async Task<IActionResult> Register(DoctorRegisterRequest request)
        {
            var response = await _mediator.Send(new DoctorRegisterCommand(request.FirstName,
                                                              request.LastName,
                                                              request.Email,
                                                              request.DateOfBirth,
                                                              request.LicenseNumber,
                                                              request.Specialization,
                                                              request.Password,
                                                              request.ConfirmationPassword));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginrRequest request)
        {
            var response = await _mediator.Send(new LoginCommand(request.Email, request.Password));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string? Email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(Email))
                return Unauthorized();

            var response = await _mediator.Send(new LogoutCommand(Email));

            return response.IsSuccess ? NoContent() : HandleFailure(response);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> GenerateJwtToken(GenerateJwtTokenRequest request)
        {
            var response = await _mediator.Send(new GenerateJwtTokenCommand(request.Token, request.RefreshToken));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
    }
}
