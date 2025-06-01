using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurnat.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            //arrange
            
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();
            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantRepositoryMock.Setup(repo =>
                repo.CreateAsync(It.IsAny<Restaurant>()))
                    .ReturnsAsync(1);
            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("owner-Id", string.Empty, [], null, null);
            userContextMock.Setup(u=>u.GetCurrentUser()).Returns(currentUser);
            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
            mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

            var commandHandler = new CreateRestaurantCommandHandler(
                restaurantRepositoryMock.Object,
                loggerMock.Object,
                mapperMock.Object,
                userContextMock.Object);

            //act

            var result = await commandHandler.Handle(command, CancellationToken.None);

            //assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("owner-Id");
            restaurantRepositoryMock.Verify(r=>r.CreateAsync(restaurant), Times.Once());
        }
    }
}