using System.Linq.Expressions;
using FluentValidation;

namespace DineHub.Application.Commands.Validators.Abstraction;

public class RestaurantValidatorBase<T> : AbstractValidator<T> where T : class
{
        public RestaurantValidatorBase(
            Expression<Func<T, string>> nameExpression,
            Expression<Func<T, string>> emailExpression,
            Expression<Func<T, string>> postalCodeExpression)
        {
            RuleFor(nameExpression)
                .Length(3, 100)
                .WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(emailExpression)
                .EmailAddress()
                .WithMessage("Enter a valid email address.");

            RuleFor(postalCodeExpression)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a valid postal code (XX-XXX).");
        }
}