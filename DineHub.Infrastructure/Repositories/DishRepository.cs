using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DineHub.Infrastructure.Repositories;

public class DishRepository(ApplicationDbContext context) : IDishRepository
{
    public async Task<Guid> AddDishAsync(Dish dish)
    {
        await context.Dishes.AddAsync(dish);

        await context.SaveChangesAsync();

        return dish.Id;
    }

    public async Task DeleteDishAsync(Restaurant restaurant, Dish dish)
    {
        restaurant.Dishes.Remove(dish);

        await context.SaveChangesAsync();
    }
}