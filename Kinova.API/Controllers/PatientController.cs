using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Application.Features.Patients.Queries.GetActivePlans;
using Kinova.Application.Features.Patients.Queries.GetPatientDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kinova.API.Controllers
{
    public class PatientController : APIController
    {
        public PatientController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("me/plans/active")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetActivePlans()
        {
            var response = await _mediator.Send(new GetActivePlansQuery(UserId));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
        [HttpGet("me")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetDetails()
        {
            var response = await _mediator.Send(new GetPatientDetailsQuery(UserId));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
    }
}
