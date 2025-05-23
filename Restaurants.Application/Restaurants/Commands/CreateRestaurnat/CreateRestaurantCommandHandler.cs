﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurnat
{
    internal class CreateRestaurantCommandHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new restaurant {@Restaurant}", request);
            var restaurant = mapper.Map<Restaurant>(request);
            int id = await restaurantsRepository.CreateAsync(restaurant);
            return id;
        }
    }
}
