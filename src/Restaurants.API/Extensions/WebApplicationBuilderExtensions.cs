using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "bearerAuth"
                            }
                        },
                        []
                    }
                });
            });
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();


            var aiConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");
            if (string.IsNullOrEmpty(aiConnectionString))
            {
                Console.WriteLine("______________________________________");
                Console.WriteLine("Warning: Application Insights connection string not set. Skipping telemetry setup.");
                Console.WriteLine("______________________________________");
            }
            //check for inegration tests
            if (!string.IsNullOrEmpty(aiConnectionString))
            {
                builder.Host.UseSerilog((context, configuration) =>
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .WriteTo.ApplicationInsights(
                            new TelemetryConfiguration { ConnectionString = aiConnectionString },
                            new TraceTelemetryConverter());
                });
            }
            else
            {

            }
        }
    }
}
