﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Application.Users.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurantsCreated;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Configuration;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Storage;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");

            services.AddDbContext<RestaurantsDbContext>(options=>options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality,
                    builder => builder.RequireClaim(AppClaimTypes.Nationality, "Polish", "Indian"))
                .AddPolicy(PolicyNames.AtLeast20,
                    builder=>builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.OwnesAtLeast2,
                    builder => builder.AddRequirements(new MinimumRestaurantsCreatedRequirement(2))); 

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, MinimumRestaurantsCreatedRequirementHandler>();

            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
            var blobCon = Environment.GetEnvironmentVariable("AZURE_BLOB_CONNECTION_STRING");
            var blobAccountKeyString = Environment.GetEnvironmentVariable("AZURE_BLOB_ACCOUNTKEY");

            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));

            services.PostConfigure<BlobStorageSettings>(settings =>
            {
                settings.ConnectionString = blobCon;
                settings.AccountKey = blobAccountKeyString;
            });

            services.AddScoped<IBlobStorageService, BlobStorageService>();

            if (!string.IsNullOrEmpty(blobCon) && !string.IsNullOrEmpty(blobAccountKeyString))
            {
                
            }
            else
            {
                Console.WriteLine("AZURE_BLOB_CONNECTION_STRING or AZURE_BLOB_ACCOUNT_Key not set");
            }


        }
    }
}
