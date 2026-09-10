using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Application.Features.Patients.Queries.GetActivePlans;
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
        [Authorize]
        public async Task<IActionResult> GetActivePlans()
        {
            var response = await _mediator.Send(new GetActivePlansQuery(UserId));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
    }
}
