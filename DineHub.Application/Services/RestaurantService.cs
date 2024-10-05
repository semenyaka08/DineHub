using AutoMapper;
using DineHub.Application.Dtos;
using DineHub.Application.Dtos.RestaurantDtos;
using DineHub.Application.Services.Interfaces;
using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Services;

public class RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger, IMapper mapper) : IRestaurantService
{
    public async Task<List<GetRestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants!");

        var restaurant = await restaurantRepository.GetAllRestaurants();

        var restaurantsDto = mapper.Map<List<GetRestaurantDto>>(restaurant);

        return restaurantsDto;
    }

    public async Task<GetRestaurantDto?> GetById(Guid id)
    {
        logger.LogInformation($"Getting restaurant with id: {id}");

        var restaurant = await restaurantRepository.GetById(id);

        if (restaurant == null)
            return null;

        var restaurantDto = mapper.Map<GetRestaurantDto>(restaurant);

        return restaurantDto;
    }

    public async Task<Guid> AddRestaurant(CreateRestaurantDto restaurantDto)
    {
        logger.LogInformation($"Creating a restaurant with name {restaurantDto.Name}");
        
        var restaurant = mapper.Map<Restaurant>(restaurantDto);

        Guid id = await restaurantRepository.AddRestaurant(restaurant);

        return id;
    }
}