using DineHub.Domain.Entities;

namespace DineHub.Domain.RepositoryContracts;

public interface IDishRepository
{
    Task<Guid> AddDishAsync(Dish dish);

    Task DeleteDishAsync(Restaurant restaurant, Dish dish);
}