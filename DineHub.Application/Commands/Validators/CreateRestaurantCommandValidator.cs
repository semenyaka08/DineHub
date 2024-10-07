using DineHub.Application.Commands.Restaurants;
using DineHub.Application.Commands.Validators.Abstraction;
using FluentValidation;

namespace DineHub.Application.Commands.Validators;

public class CreateRestaurantCommandValidator()
    : RestaurantValidatorBase<CreateRestaurantCommand>(z => z.Name, z => z.ContactEmail, z => z.PostalCode);
