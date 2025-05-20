using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants
{
    public interface IRestauarntService
    {
        Task<IEnumerable<RestaurantDto>> GetAllAsync();
        Task<RestaurantDto> GetByIdAsync(int id);
    }
}
