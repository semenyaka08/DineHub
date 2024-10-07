using DineHub.Application.Commands.Dishes;
using FluentValidation;

namespace DineHub.Application.Commands.Validators;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(z => z.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(z => z.KiloCalories)
            .GreaterThan(0)
            .WithMessage("KiloCalories must be greater than 0");
    }
}