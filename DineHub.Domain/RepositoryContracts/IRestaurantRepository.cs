using DineHub.Domain.Entities;

namespace DineHub.Domain.RepositoryContracts;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllRestaurantsAsync();

    Task<Restaurant?> GetByIdAsync(Guid id);

    Task<Guid> AddRestaurantAsync(Restaurant restaurant);

    Task<bool> DeleteRestaurantAsync(Restaurant restaurant);

    Task<bool> SaveChangesAsync();
}