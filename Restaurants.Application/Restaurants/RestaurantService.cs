using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    public class RestaurantService(
        IRestaurantsRepository restaurantsRepository,
        ILogger<RestaurantService> logger,
        IMapper mapper) : IRestauarntService
    {
        public async Task<int> CreateAsync(CreateRestaurantDto dto)
        {
            logger.LogInformation("Creating new restaurant");
            var restaurant = mapper.Map<Restaurant>(dto);
            int id = await restaurantsRepository.CreateAsync(restaurant);
            return id;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();
            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantsDtos;
        }

        public async Task<RestaurantDto> GetByIdAsync(int id)
        {
            logger.LogInformation($"Getting restaurant {id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(id);
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
