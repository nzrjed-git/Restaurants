using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteAllForRestaurant;
using Restaurants.Application.Dishes.Queries.GetAllForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants{restaurantId}/dishes")]
    public class DishesController
        (IMediator mediator): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish(int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            int id = await mediator.Send(command);
            return Created();
            //return CreatedAtAction(nameof(GetById), new { restaurantId, id }, null);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllForRestaurant(int restaurantId)
        {
            var query = new GetAllForRestaurantQuery { RestaurantId = restaurantId };
            var dishes = await mediator.Send(query);
            return Ok(dishes);
        }
        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetDishForRestaurant(int restaurantId, int dishId)
        {
            var query = new GetDishByIdForRestaurantQuery { RestaurantId = restaurantId, DishId = dishId };
            var dish = await mediator.Send(query);
            return Ok(dish);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDish(int restaurantId)
        {
            await mediator.Send(new DeleteAllForRestaurantCommand { RestaurantId = restaurantId });
            return NoContent();
        }
    }
}
