using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    internal class DeleteRestaurantCommandHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<DeleteRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {

        async Task IRequestHandler<DeleteRestaurantCommand>.Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("@Deleting restaurant with id: {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbidException();

            await restaurantsRepository.Delete(restaurant);
        }
    }
}
