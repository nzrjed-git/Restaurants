
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);
        Task<IEnumerable<Restaurant>> GetAllByOwnerIdAsync(string userId);
        Task<Restaurant?> GetByIdAsync(int id);
        Task<int> CreateAsync(Restaurant restaurant);
        Task Delete(Restaurant restaurant);
        Task SaveChangesAsync();
    }
}
