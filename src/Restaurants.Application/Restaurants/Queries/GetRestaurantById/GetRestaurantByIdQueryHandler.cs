﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    internal class GetRestaurantByIdQueryHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper,
        IBlobStorageService blobStorageService) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting restaurant {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            restaurantDto.LogogSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);
            return restaurantDto;
        }
    }
}
