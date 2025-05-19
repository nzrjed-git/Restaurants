using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task SeedAsync()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            return new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Sunrise Diner",
                    Description = "Cozy spot for all-day breakfast and brunch.",
                    Category = "Diner",
                    HasDelivery = false,
                    ContactEmail = "contact@sunrisediner.com",
                    ContactNumber = "+1-555-1234",
                    Address = new Address
                    {
                        City = "Springfield",
                        Street = "123 Main St",
                        PostalCode = "98765"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Pancake Stack",
                            Description = "Fluffy pancakes served with maple syrup.",
                            Price = 5.99m,
                            KiloCalories = 400
                        },
                        new Dish
                        {
                            Name = "Veggie Omelette",
                            Description = "Three-egg omelette with seasonal vegetables.",
                            Price = 7.49m,
                            KiloCalories = 240
                        }
                    }
                },
                new Restaurant
                {
                    Name = "La Trattoria",
                    Description = "Authentic Italian cuisine in a warm atmosphere.",
                    Category = "Italian",
                    HasDelivery = true,
                    ContactEmail = "info@latrattoria.com",
                    ContactNumber = "+1-555-5678",
                    Address = new Address
                    {
                        City = "Rivertown",
                        Street = "456 River Rd",
                        PostalCode = "12345"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Margherita Pizza",
                            Description = "Classic pizza with tomato, mozzarella, and basil.",
                            Price = 12.00m,
                            KiloCalories = 600
                        },
                        new Dish
                        {
                            Name = "Spaghetti Carbonara",
                            Description = "Spaghetti with eggs, Pecorino Romano, and pancetta.",
                            Price = 14.50m,
                            KiloCalories = 420
                        },
                        new Dish
                        {
                            Name = "Tiramisu",
                            Description = "Espresso-soaked ladyfingers layered with mascarpone.",
                            Price = 6.50m,
                            KiloCalories = 375
                        }
                    }
                }
            };

        }
    }
}
