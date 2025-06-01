using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurantsCreated
{
    internal class MinimumRestaurantsCreatedRequirement(int restautnatsCount) : IAuthorizationRequirement
    {
        public int RestautnatsCount { get; } = restautnatsCount;
    }
}
