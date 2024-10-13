using DineHub.Application.Dtos.DishDtos;
using MediatR;

namespace DineHub.Application.Queries.Dishes;

public class GetDishByIdQuery(Guid id, Guid restaurantId) : IRequest<DishDto>
{
    public Guid Id { get; } = id;

    public Guid RestaurantId { get; } = restaurantId;
}