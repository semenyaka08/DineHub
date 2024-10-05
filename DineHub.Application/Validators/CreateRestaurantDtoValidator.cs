using DineHub.Application.Dtos.RestaurantDtos;
using FluentValidation;

namespace DineHub.Application.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
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