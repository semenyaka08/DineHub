using DineHub.Application.Dtos;
using DineHub.Application.Dtos.RestaurantDtos;
using DineHub.Domain.Entities;

namespace DineHub.Application.Services.Interfaces;

public interface IRestaurantService
{
    Task<List<GetRestaurantDto>> GetAllRestaurants();
    Task<GetRestaurantDto?> GetById(Guid id);

    Task<Guid> AddRestaurant(CreateRestaurantDto restaurantDto);
}