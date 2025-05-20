using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Validators;

namespace Restaurants.Application.Extensisons
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicatin(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddScoped<IRestauarntService, RestaurantService>();
            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly)
                .AddFluentValidationAutoValidation();

        }
    }
}
