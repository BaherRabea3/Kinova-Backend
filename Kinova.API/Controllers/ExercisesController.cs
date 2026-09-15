using Kinova.Application.Features.Exercises.Queries.GetExerciseById;
using Kinova.Application.Features.Exercises.Queries.GetExercises;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kinova.API.Controllers
{
    public class ExercisesController : APIController
    {
        public ExercisesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetExercises(
            [FromQuery] string? search = null,
            [FromQuery] string? category = null,
            [FromQuery] string? bodyPart = null,
            [FromQuery] string? difficultyLevel = null)
        {
            var response = await _mediator.Send(new GetExerciseQeury(search, category, bodyPart, difficultyLevel));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetExerciseByIdQuery(id));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
    }
}
