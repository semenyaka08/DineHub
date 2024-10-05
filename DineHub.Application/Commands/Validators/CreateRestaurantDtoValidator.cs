using DineHub.Application.Commands.Restaurants;
using FluentValidation;

namespace DineHub.Application.Commands.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(z=>z.Name)
            .Length(3, 100);

        RuleFor(z=>z.ContactEmail)
            .EmailAddress()
            .WithMessage("Enter a valid email address");

        RuleFor(z => z.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX).");
    }
}