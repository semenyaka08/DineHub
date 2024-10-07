using MediatR;

namespace DineHub.Application.Commands.Dishes;

public class DeleteDishCommand(Guid id, Guid restaurantId) : IRequest
{
    public Guid Id { get; } = id;

    public Guid RestaurantId { get; } = restaurantId;
}