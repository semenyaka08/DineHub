using DineHub.Domain.Entities;

namespace DineHub.Domain.RepositoryContracts;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllRestaurants();

    Task<Restaurant?> GetById(Guid id);
}