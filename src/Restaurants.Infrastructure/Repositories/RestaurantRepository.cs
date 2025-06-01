using System.Globalization;
using System.Linq.Expressions;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
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
        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(
            string? searchPhrase,
            int pageSize,
            int pageNumber,
            string? sortBy,
            SortDirection sortDirection)
        {
            var query = dbContext.Restaurants
            .AsNoTracking()
            .Include(r => r.Dishes)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                var search = searchPhrase.ToLower();

                query = query.Where(r =>
                    r.Name.Contains(search)
                    || r.Description.Contains(search));
            }
            var totalCount = await query.CountAsync();
            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r => r.Name },
                    { nameof(Restaurant.Description), r => r.Description },
                    { nameof(Restaurant.Category), r => r.Category },
                };
                var selectedColumn = columnsSelector[sortBy];

                query = 
                    sortDirection == SortDirection.Ascending
                    ? query.OrderBy(selectedColumn)
                    : query.OrderByDescending(selectedColumn);
            }

            var restaurants = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
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
