﻿using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurnat;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.DTOs.Tests
{
    public class RestaurantsProfileTests
    {
        private readonly IMapper _mapper;

        public RestaurantsProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

            _mapper = configuration.CreateMapper();

        }
        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            //arrange
            
            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "Test name",
                Description = "Test desc",
                Category = "Test cat",
                HasDelivery = true,
                ContactEmail = "Test@test.com",
                ContactNumber = "123456789",
                Address = new Address
                {
                    City = "Test city",
                    Street = "Test str",
                    PostalCode = "12345",
                }
            };

            //act 

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            //assert

            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.ContactEmail.Should().Be(restaurant.ContactEmail);
            restaurantDto.ContactNumber.Should().Be(restaurant.ContactNumber);
            restaurantDto.City.Should().Be(restaurant.Address.City);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            //arrange

            var command = new UpdateRestaurantCommand()
            {
                Id = 1,
                Name = "Test name",
                Description = "Test desc",
                HasDelivery = true,
            };

            //act 

            var restaurant = _mapper.Map<Restaurant>(command);

            //assert

            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
        }

        [Fact()]
        public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            //arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "Test name",
                Description = "Test desc",
                Category = "Test cat",
                HasDelivery = true,
                ContactEmail = "Test@test.com",
                ContactNumber = "123456789",
                City = "Test city",
                Street = "Test str",
                PostalCode = "12345",
            };

            //act 

            var restaurant = _mapper.Map<Restaurant>(command);

            //assert

            restaurant.Should().NotBeNull();

            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Category.Should().Be(command.Category);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
            restaurant.ContactEmail.Should().Be(command.ContactEmail);
            restaurant.ContactNumber.Should().Be(command.ContactNumber);
            restaurant.Address!.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        }
    }
}