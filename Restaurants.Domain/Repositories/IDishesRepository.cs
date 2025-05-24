using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        //Task<IEnumerable<Dish>> GetAllByRestaurantIdAsync(int restaurantId);
        //Task<Dish?> GetByIdAsync(int restaurantId, int id);
        Task<int> CreateAsync(Dish dish);
        Task DeleteForRestaurantAsync(IEnumerable<Dish> dishes);
        //Task SaveChangesAsync();
    }
}
