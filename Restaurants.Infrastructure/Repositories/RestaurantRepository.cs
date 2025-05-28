using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantRepository(
        RestaurantsDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<int> CreateAsync(Restaurant restaurant)
        {
            await dbContext.Restaurants.AddAsync(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task Delete(Restaurant restaurant)
        {
            dbContext.Remove(restaurant);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.AsNoTracking()
                .Include(r => r.Dishes)
                .ToListAsync();
            return restaurants;
        }
        public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            if (searchPhraseLower != null)
            {
                var restaurants = await dbContext.Restaurants.AsNoTracking()
                    .Where(r =>
                    (r.Name.Contains(searchPhraseLower)
                    || r.Description.Contains(searchPhraseLower)))
                .Include(r => r.Dishes)
                .ToListAsync();
                return restaurants;
            }
            else
            {
                var restaurants = await dbContext.Restaurants.AsNoTracking()
                .Include(r => r.Dishes)
                .ToListAsync();
                 return restaurants;
            }
        }

        public async Task<IEnumerable<Restaurant>> GetAllByOwnerIdAsync(string ownerId)
        {
            var restaurants = await dbContext.Restaurants.AsNoTracking()
                .Where(r=>r.OwnerId == ownerId)
                .ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefaultAsync(r=>r.Id == id);
            return restaurant;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
