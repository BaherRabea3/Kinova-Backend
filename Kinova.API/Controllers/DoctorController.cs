using Kinova.API.Requests.Doctor;
using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Application.Features.Doctors.Commands.CreatePlan;
using Kinova.Application.Features.Doctors.Commands.UpdateDoctorProfile;
using Kinova.Application.Features.Doctors.Commands.UpdatePlan;
using Kinova.Application.Features.Doctors.Queries.GetDashboardSummary;
using Kinova.Application.Features.Doctors.Queries.GetDoctorProfile;
using Kinova.Application.Features.Doctors.Queries.GetPatientDetails;
using Kinova.Application.Features.Doctors.Queries.GetPatientPlans;
using Kinova.Application.Features.Doctors.Queries.GetPatientReports;
using Kinova.Application.Features.Doctors.Queries.GetPatients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kinova.API.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : APIController
    {
        public DoctorController(IMediator mediator) : base(mediator)
        {
        }

        private string Email =>
            User.FindFirstValue(ClaimTypes.Email)
            ?? throw new UnauthorizedAccessException("Email claim missing from token.");

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var response = await _mediator.Send(new GetDoctorProfileQuery(UserId, Email));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateDoctorProfileRequest request)
        {
            var response = await _mediator.Send(new UpdateDoctorProfileCommand(
                UserId,
                Email,
                request.Name,
                request.LicenseNumber,
                request.Specialization));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetPatients(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? status = null)
        {
            var response = await _mediator.Send(new GetPatientsQuery(UserId, page, pageSize, search, status));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpGet("patients/{id:guid}")]
        public async Task<IActionResult> GetPatientDetails([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetDoctorPatientDetailsQuery(UserId, id));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpGet("patients/{id:guid}/reports")]
        public async Task<IActionResult> GetPatientReports([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetPatientReportsQuery(UserId, id));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var response = await _mediator.Send(new GetDashboardSummaryQuery(UserId));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpGet("patients/{id:guid}/plans")]
        public async Task<IActionResult> GetPatientPlans([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetPatientPlansQuery(UserId, id));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpPost("plans")]
        public async Task<IActionResult> CreatePlan(CreatePlanRequest request)
        {
            var response = await _mediator.Send(new CreatePlanCommand(
                UserId,
                request.PatientId,
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.IsActive,
                request.Exercises.Select(e => new PlanExerciseItemInputDto
                {
                    ExerciseId = e.ExerciseId,
                    Sets = e.Sets,
                    Repetitions = e.Repetitions,
                    FrequencyPerWeek = e.FrequencyPerWeek
                }).ToList()));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        [HttpPut("plans/{id:guid}")]
        public async Task<IActionResult> UpdatePlan([FromRoute] Guid id, UpdatePlanRequest request)
        {
            var response = await _mediator.Send(new UpdatePlanCommand(
                UserId,
                id,
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.IsActive));

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
    }
}
