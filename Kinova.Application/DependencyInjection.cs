using FluentValidation;
using Kinova.Application;
using Kinova.Application.Behaviours;
using Kinova.Application.Common.Helpers;
using Kinova.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<IScoreCalculator, ScoreCalculator>();
            services.AddScoped<IReportContentBuilder, ReportContentBuilder>();

            return services;
        }
    }
}
