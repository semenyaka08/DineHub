using DineHub.Domain.Entities;
using DineHub.Domain.Enums;

namespace DineHub.Domain.RepositoryContracts;

public interface IRestaurantRepository
{
    Task<(List<Restaurant>, int itemsCount)> GetAllMatchingRestaurantsAsync(string? searchString, int pageSize, int pageNumber, string sortItem, SortOrder sortOrder);
    
    Task<List<Restaurant>> GetAllRestaurantsAsync();

    Task<Restaurant?> GetByIdAsync(Guid id);

    Task<Guid> AddRestaurantAsync(Restaurant restaurant);

    Task<bool> DeleteRestaurantAsync(Restaurant restaurant);

    Task<bool> SaveChangesAsync();
}