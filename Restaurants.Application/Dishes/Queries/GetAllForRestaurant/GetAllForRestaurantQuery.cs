using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetAllForRestaurant
{
    public class GetAllForRestaurantQuery() : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; set; }
    }
}
