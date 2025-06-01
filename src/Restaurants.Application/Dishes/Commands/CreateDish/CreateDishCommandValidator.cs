using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator: AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator() {
            RuleFor(d => d.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name can't be longer than 100 characters.");

            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(300).WithMessage("Description can't be longer than 300 characters.");

            RuleFor(d => d.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(d => d.KiloCalories)
                .GreaterThanOrEqualTo(0).When(d => d.KiloCalories.HasValue)
                .WithMessage("KiloCalories must be non-negative.");

            RuleFor(d => d.RestaurantId)
                .GreaterThan(0).WithMessage("RestaurantId must be greater than 0.");
        }
    }
}
