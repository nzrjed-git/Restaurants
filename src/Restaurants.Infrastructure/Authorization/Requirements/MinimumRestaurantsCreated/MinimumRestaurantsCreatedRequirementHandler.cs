using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurantsCreated
{
    internal class MinimumRestaurantsCreatedRequirementHandler(
        ILogger<MinimumRestaurantsCreatedRequirementHandler> logger,
        IUserContext userContext,
        IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<MinimumRestaurantsCreatedRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsCreatedRequirement requirement)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("User {UserEmail} handling MinimumRestaurantsCreatedRequirement", user!.Email);
            var userRestaurants = await restaurantsRepository.GetAllByOwnerIdAsync(user.Id);
            int userRestaurantsCount = userRestaurants.Count();
            if (userRestaurantsCount >= requirement.RestautnatsCount)
            {
                context.Succeed(requirement);
            }
            else
            {
                logger.LogWarning("User ownes less than {requirementCount} restaurants", requirement.RestautnatsCount);
                context.Fail();
            }
        }
    }
}
