﻿
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<int> CreateAsync(Restaurant restaurant);
        Task Delete(Restaurant restaurant);
        Task SaveChangesAsync();
    }
}
