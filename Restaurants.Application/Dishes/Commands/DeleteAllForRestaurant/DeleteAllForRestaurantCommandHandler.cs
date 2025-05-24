using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Queries.GetAllForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteAllForRestaurant
{
    internal class DeleteAllForRestaurantCommandHandler(
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        ILogger<GetAllForRestaurantQueryHandler> logger,
        IMapper mapper) : IRequestHandler<DeleteAllForRestaurantCommand>
    {
        public async Task Handle(DeleteAllForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Deleting all dishes for restaurant with id: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dishes = restaurant.Dishes;
            if (dishes != null) await dishesRepository.DeleteForRestaurantAsync(dishes);
        }
    }
}
