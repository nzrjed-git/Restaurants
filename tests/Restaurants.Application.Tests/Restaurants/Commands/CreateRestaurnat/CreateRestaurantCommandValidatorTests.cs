using System.ComponentModel.DataAnnotations;
using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurnat.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "name",
                Description = "description",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert

            result.ShouldNotHaveAnyValidationErrors();  
        }

        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
        {
            //arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "na",

                Category = "Ita",
                ContactEmail = "testtest.com",
                PostalCode = "12345",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert

            result.ShouldHaveValidationErrorFor(c=>c.Name);
            result.ShouldHaveValidationErrorFor(c=>c.Description);
            result.ShouldHaveValidationErrorFor(c=>c.Category);
            result.ShouldHaveValidationErrorFor(c=>c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c=>c.PostalCode);
        }

        [Theory]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Japaneese")]
        [InlineData("American")]
        [InlineData("Indian")]
        public void Validator_ForValidCategory_ShouldNotHaveValiadtionErrorsForCategoryProperty(string category)
        {
            //arrange

            var validator = new CreateRestaurantCommandValidator();

            var command = new CreateRestaurantCommand() { Category = category };

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveValidationErrorFor(c=>c.Category);
        }
        [Theory]
        [InlineData("12345")]
        [InlineData("123-45")]
        [InlineData("12 345")]
        [InlineData("12-3 45")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
        {
            //assert
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand()
            {
                PostalCode = postalCode
            };

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }
    }
}