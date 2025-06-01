using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> CreateAsync(Dish dish)
        {
            await dbContext.AddAsync(dish);
            await dbContext.SaveChangesAsync();
            return dish.Id;
        }

        public async Task DeleteForRestaurantAsync(IEnumerable<Dish> dishes)
        {
            dbContext.RemoveRange(dishes);
            await dbContext.SaveChangesAsync();
        }
    }
}
