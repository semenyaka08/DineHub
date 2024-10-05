using AutoMapper;
using DineHub.Application.Queries.Restaurants.Dtos;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Queries.Restaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, List<GetRestaurantDto>>
{
    public async Task<List<GetRestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants!");

        var restaurant = await restaurantRepository.GetAllRestaurants();

        var restaurantsDto = mapper.Map<List<GetRestaurantDto>>(restaurant);

        return restaurantsDto;
    }
}