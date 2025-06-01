using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteAllForRestaurant
{
    public class DeleteAllForRestaurantCommand: IRequest
    {
        public int RestaurantId { get; set; }
    }
}
