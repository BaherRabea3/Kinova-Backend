using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Kinova.API.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
           _logger.LogError(exception,"Exception occured: {Message}",exception.Message);

            var problem = exception switch
            {
                UnauthorizedAccessException => new ProblemDetails
                {
                    Title = "Unauthorized.",
                    Detail = "Authentication is required.",
                    Status = StatusCodes.Status401Unauthorized
                },

                _ => new ProblemDetails
                {
                    Title = "Internal Server Error.",
                    Detail = "An unexpected error occurred.",
                    Status = StatusCodes.Status500InternalServerError
                }
            };

            httpContext.Response.StatusCode = problem.Status!.Value;

            await httpContext.Response.WriteAsJsonAsync(problem,cancellationToken);

            return true;

        }
    }
}
