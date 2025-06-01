using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQuery: IRequest<DishDto?>
    {
        public int DishId { get; set; }
        public int RestaurantId { get; set; }
    }
}
