using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    internal class CreateDishCommandHandler(
        IDishesRepository dishesRepository,
        IRestaurantsRepository restaurantsRepository,
        ILogger<CreateDishCommandHandler> logger,
        IMapper mapper,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating dish: @{DishRequest} for restaurant with id: {RestaurantId}", request, request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbidException();
            var dish = mapper.Map<Dish>(request);
            int id = await dishesRepository.CreateAsync(dish);
            return id;
        }
    }
}
