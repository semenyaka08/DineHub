using DineHub.Application.Dtos;
using DineHub.Domain.Entities;

namespace DineHub.Application.Services.Interfaces;

public interface IRestaurantService
{
    Task<List<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetById(Guid id);
}