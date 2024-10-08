using AutoMapper;
using DineHub.Application.Dtos.RestaurantDtos;
using DineHub.Application.User;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Queries.Restaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, List<GetRestaurantDto>>
{
    public async Task<List<GetRestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants!");

        var restaurants = await restaurantRepository.GetAllRestaurantsAsync();

        var restaurantsDto = mapper.Map<List<GetRestaurantDto>>(restaurants);

        return restaurantsDto;
    }
}