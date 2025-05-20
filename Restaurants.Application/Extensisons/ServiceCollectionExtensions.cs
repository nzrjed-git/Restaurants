using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extensisons
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicatin(this IServiceCollection services)
        {
            services.AddScoped<IRestauarntService, RestaurantService>();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
