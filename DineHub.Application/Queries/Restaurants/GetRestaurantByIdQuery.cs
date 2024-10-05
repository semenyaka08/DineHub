using DineHub.Application.Queries.Restaurants.Dtos;
using MediatR;

namespace DineHub.Application.Queries.Restaurants;

public class GetRestaurantByIdQuery(Guid id) : IRequest<GetRestaurantDto?>
{
    public Guid Id { get; } = id;
}