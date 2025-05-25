using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.User;

namespace Restaurants.Application.Extensisons
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly)
                .AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();
        }
    }
}
