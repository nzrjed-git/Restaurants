using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteAllForRestaurant;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetAllForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
    [Authorize]
    public class DishesController
        (IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish(int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            int dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDishForRestaurant), new { restaurantId, dishId }, null);
        }
        [HttpGet]
        [Authorize(Policy = PolicyNames.AtLeast20)]
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
