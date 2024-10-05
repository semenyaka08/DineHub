using AutoMapper;
using DineHub.Application.Dtos;
using DineHub.Application.Services.Interfaces;
using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Services;

public class RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger, IMapper mapper) : IRestaurantService
{
    public async Task<List<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants!");

        var restaurant = await restaurantRepository.GetAllRestaurants();

        var restaurantsDto = mapper.Map<List<RestaurantDto>>(restaurant);

        return restaurantsDto;
    }

    public async Task<RestaurantDto?> GetById(Guid id)
    {
        logger.LogInformation($"Getting restaurant with id: {id}");

        var restaurant = await restaurantRepository.GetById(id);

        if (restaurant == null)
            return null;

        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
}