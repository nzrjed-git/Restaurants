using System.Security.AccessControl;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;

        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

            _handler = new UpdateRestaurantCommandHandler(
                _restaurantsRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object,
                _restaurantAuthorizationServiceMock.Object);
        }
        [Fact()]
        public async Task Handle_ForValidCommand_ShouldUpdateRestaurants()
        {
            //arrange
            var restaurantId = 1;
            var request = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "new Test name",
                Description = " new Test description",
                HasDelivery = true,
            };

            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test",
                HasDelivery = false
            };

            _restaurantsRepositoryMock.Setup(r=>r.GetByIdAsync(restaurantId))
                .ReturnsAsync(restaurant);
            _restaurantAuthorizationServiceMock.Setup(s=>s.Authorize(restaurant, ResourceOperation.Update))
                .Returns(true);

            //act

            await _handler.Handle(request, CancellationToken.None);

            //assert
            _restaurantsRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mapperMock.Verify(m=>m.Map(request, restaurant), Times.Once);
        }

        [Fact()]
        public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
        {
            //arrange
            var restaurantId = 2;
            var request = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
            };
            _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
                .ReturnsAsync((Restaurant?)null);
            //act

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            //assert

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurant with id: {restaurantId} does not exist");
        }

        [Fact()]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            //arrange
            var restaurantId = 3;
            var request = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
            };
            var restaurant = new Restaurant()
            {
                Id = restaurantId,
            };
            _restaurantsRepositoryMock.Setup( r => r.GetByIdAsync(restaurantId))
                .ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock
                .Setup(s => s.Authorize(restaurant, ResourceOperation.Update))
                .Returns(false);
            //act

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            //assert

            await act.Should().ThrowAsync<ForbidException>();
        }
    }
}