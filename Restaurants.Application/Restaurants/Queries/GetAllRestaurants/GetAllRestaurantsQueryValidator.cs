﻿using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowedPageSizes = [5, 10, 15, 30];
        private string[] allowSortByColumnNames =
        [
            nameof(RestaurantDto.Name),
            nameof(RestaurantDto.Category),
            nameof(RestaurantDto.Description)
        ];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => allowedPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");

            RuleFor(r => r.SortBy)
                .Must(value => allowSortByColumnNames.Contains(value))
                .When(q=>q.SortBy != null)
                .WithMessage($"Sort by is optional or must be in [{string.Join(",", allowSortByColumnNames)}]");
        }
    }
}
