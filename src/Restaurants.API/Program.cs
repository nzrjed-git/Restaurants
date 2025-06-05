using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensisons;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = CreateHostBuilder(args);

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
        await seeder.SeedAsync();

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestTimeLoggingMiddleware>();
        var aiConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");
        if (!string.IsNullOrEmpty(aiConnectionString))
        {
            app.UseSerilogRequestLogging();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGroup("api/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        app.UseAuthorization();
        app.MapControllers();

        await app.RunAsync();
    }

    public static WebApplicationBuilder CreateHostBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddPresentation();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        return builder;
    }
}
