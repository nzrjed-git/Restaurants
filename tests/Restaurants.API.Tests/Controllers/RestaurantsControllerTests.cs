using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.API.Tests.Controllers
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();

        public RestaurantsControllerTests(WebApplicationFactory<Program> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.Replace(ServiceDescriptor.Scoped(
                            typeof(IRestaurantsRepository), _ => _restaurantsRepositoryMock.Object));
                    });
                });
        }

        [Fact()]
        public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
        {
            //arrange
            var id = 123;

            _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);
            var client = _webApplicationFactory.CreateClient();

            //act
            var response = await client.GetAsync($"/api/restaurants/{id}");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Fact()]
        public async Task GetById_ForExistingId_ShouldReturn200Ok()
        {
            //arrange
            var id = 321;
            var restaurant = new Restaurant()
            {
                Id = id,
                Name = "Test",
                Description = "Test desc",
            };
            _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(restaurant);
            var client = _webApplicationFactory.CreateClient();

            //act
            var response = await client.GetAsync($"/api/restaurants/{id}");
            var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be("Test");
            restaurantDto.Description.Should().Be("Test desc");
        }

        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            //arrange
            var client = _webApplicationFactory.CreateClient();

            //act
            var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact()]
        public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
        {
            //arrange
            var client = _webApplicationFactory.CreateClient();

            //act
            var result = await client.GetAsync("/api/restaurants");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}