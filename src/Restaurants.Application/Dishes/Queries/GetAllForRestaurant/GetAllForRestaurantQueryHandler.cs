using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllForRestaurant
{
    internal class GetAllForRestaurantQueryHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<GetAllForRestaurantQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetAllForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetAllForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all dishes of restaurant with id: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dishesDto = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return dishesDto;
        }
    }
}
