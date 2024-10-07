using DineHub.Application.Dtos.RestaurantDtos;
using MediatR;

namespace DineHub.Application.Queries.Restaurants;

public class GetRestaurantByIdQuery(Guid id) : IRequest<GetRestaurantDto>
{
    public Guid Id { get; } = id;
}