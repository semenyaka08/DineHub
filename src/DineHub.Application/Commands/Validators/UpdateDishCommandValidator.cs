using DineHub.Application.Commands.Dishes;
using DineHub.Application.Commands.Validators.Abstraction;
using FluentValidation;

namespace DineHub.Application.Commands.Validators;

public class UpdateDishCommandValidator() : DishValidatorBase<CreateDishCommand>(z => z.Price, z => z.KiloCalories);