
using Kinova.Application.Common.DTOs.ScoreDTOs;
using Kinova.Application.Common.DTOs.SessionDTOs;
using Kinova.Application.Features.Sessions.Commands.CancelSession;
using Kinova.Application.Features.Sessions.Commands.CompleteSession;
using Kinova.Application.Features.Sessions.Commands.StartSession;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kinova.API.Controllers
{
    public class SessionController : APIController
    {
        public SessionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("{PlanExercisItemId}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Start([FromRoute] Guid PlanExercisItemId)
        {
            var result = await _mediator.Send(new StartSessionCommand(UserId, PlanExercisItemId));

            return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
        }
        [HttpPost("{id:guid}/complete")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Complete(Guid id, [FromBody] CompleteSessionRequest payload)
        {
            var result = await _mediator.Send(new CompleteSessionCommand(id, UserId, payload));
            return result.IsSuccess ? Ok(result.Value) : HandleFailure(result) ;
        }
        [HttpPost("{id:guid}/cancel")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _mediator.Send(new CancelSessionCommand(id, UserId));
            return result.IsSuccess ? NoContent() : HandleFailure(result);
        }
    }
}
