using DineHub.Application.Queries.Restaurants.Dtos;
using MediatR;

namespace DineHub.Application.Queries.Restaurants;

public class GetAllRestaurantsQuery : IRequest<List<GetRestaurantDto>>
{
    
}