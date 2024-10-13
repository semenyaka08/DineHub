using DineHub.Application.Dtos.DishDtos;
using MediatR;

namespace DineHub.Application.Queries.Dishes;

public class GetAllDishesQuery(Guid restaurantId) : IRequest<List<DishDto>>
{
    public Guid RestaurantId { get; } = restaurantId;
}