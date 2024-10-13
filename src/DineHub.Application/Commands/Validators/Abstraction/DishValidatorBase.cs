using System.Linq.Expressions;
using FluentValidation;

namespace DineHub.Application.Commands.Validators.Abstraction;

public class DishValidatorBase<T> : AbstractValidator<T> where T: class
{
    public DishValidatorBase(Expression<Func<T, decimal>> priceExpression, Expression<Func<T, int>> kiloCaloriesExpression)
    {
        RuleFor(priceExpression)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(kiloCaloriesExpression)
            .GreaterThan(0)
            .WithMessage("KiloCalories must be greater than 0");
    }
}