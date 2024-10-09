using DineHub.Application.Dtos.RestaurantDtos;
using MediatR;

namespace DineHub.Application.Queries.Restaurants;

public class GetAllRestaurantsQuery : IRequest<List<GetRestaurantDto>>
{
    public string? SearchString { get; set; }
}