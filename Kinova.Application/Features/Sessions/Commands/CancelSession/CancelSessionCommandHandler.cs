
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Patients;
using Kinova.Domain.Entities.Sessions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Sessions.Commands.CancelSession
{
    public sealed class CancelSessionCommandHandler : IRequestHandler<CancelSessionCommand, Result<Unit>>
    {
        private readonly IKinovaDbContext _context;

        public CancelSessionCommandHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(CancelSessionCommand request, CancellationToken cancellationToken)
        {
           var session = await _context.Sessions
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Id == request.SessionId, cancellationToken);

            if (session is null)
                return Result.Failure<Unit>(SessionErrors.NotFound());
            if (session.Patient.UserId != request.UserId)
                return Result.Failure<Unit>(PatientErrors.NotYourSession());

            if (session.Status == SessionStatus.Cancelled)
                return Result.Success(Unit.Value);

            if (session.Status == SessionStatus.Completed)
                return Result.Failure<Unit>(SessionErrors.CannotCanceled());

            session.EndTime = DateTime.UtcNow;
            session.Status = SessionStatus.Cancelled;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
