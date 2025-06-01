using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;


namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurantsCreated.Tests
{
    public class MinimumRestaurantsCreatedRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
        {
            //arrange
            var currentUser = new CurrentUser("1", string.Empty, [], null, null);
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    OwnerId = currentUser.Id,
                },
                new Restaurant
                {
                    OwnerId = currentUser.Id,
                }
            };
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(currentUser);

            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantRepositoryMock.Setup(r => r.GetAllByOwnerIdAsync(currentUser.Id))
                .ReturnsAsync(restaurants);

            var requirement = new MinimumRestaurantsCreatedRequirement(2);
            var loggerMock = new Mock<ILogger<MinimumRestaurantsCreatedRequirementHandler>>();
            var handler = new MinimumRestaurantsCreatedRequirementHandler(
                loggerMock.Object,
                userContextMock.Object,
                restaurantRepositoryMock.Object);

            var context = new AuthorizationHandlerContext([requirement], null, null);

            //act
            await handler.HandleAsync(context);

            //assert
            context.HasSucceeded.Should().BeTrue();
        }

        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldFail()
        {
            //arrange
            var currentUser = new CurrentUser("1", string.Empty, [], null, null);
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    OwnerId = currentUser.Id,
                },
            };
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(currentUser);

            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantRepositoryMock.Setup(r => r.GetAllByOwnerIdAsync(currentUser.Id))
                .ReturnsAsync(restaurants);

            var requirement = new MinimumRestaurantsCreatedRequirement(2);
            var loggerMock = new Mock<ILogger<MinimumRestaurantsCreatedRequirementHandler>>();
            var handler = new MinimumRestaurantsCreatedRequirementHandler(
                loggerMock.Object,
                userContextMock.Object,
                restaurantRepositoryMock.Object);

            var context = new AuthorizationHandlerContext([requirement], null, null);

            //act
            await handler.HandleAsync(context);

            //assert
            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();
        }
    }
        
}