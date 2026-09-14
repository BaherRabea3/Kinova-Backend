
using Application;
using Application.Common.Settings;
using Asp.Versioning;
using Kinova.API.Exceptions;
using Kinova.Infrastructure;
using System.Text.Json.Serialization;

namespace Kinova.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter());
                }); ;

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builderPolicy =>
                {
                    builderPolicy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST", "PUT", "DELETE");
                });
            });

            builder.Services
                   .AddApiVersioning(options =>
                   {
                       options.DefaultApiVersion = new ApiVersion(1, 0);
                       options.AssumeDefaultVersionWhenUnspecified = true;
                       options.ReportApiVersions = true;
                   })
                   .AddApiExplorer(options =>
                   {
                       options.GroupNameFormat = "'v'VVV";
                       options.SubstituteApiVersionInUrl = true;
                   });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

                app.UseSwagger();
                app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
