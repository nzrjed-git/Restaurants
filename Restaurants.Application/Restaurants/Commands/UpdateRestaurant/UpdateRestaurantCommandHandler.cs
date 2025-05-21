﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<UpdateRestaurantCommandHandler> logger,
        IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant == null) return false;
            mapper.Map(request, restaurant);
            await restaurantsRepository.SaveChangesAsync();
            return true;
        }
    }
}
