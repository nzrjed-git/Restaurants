using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetAllForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    internal class GetDishByIdForRestaurantQueryHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<GetAllForRestaurantQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery,DishDto?>
    {

        public async Task<DishDto?> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Dish with id: {DishId} for Restaurant with id: {RestaurantId}", request.DishId, request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dishDto = mapper.Map<DishDto>(restaurant.Dishes.FirstOrDefault(d=>d.Id == request.DishId));
            if (dishDto == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            return dishDto;

        }
    }
}
