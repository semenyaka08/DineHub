using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DineHub.Infrastructure.Repositories;

public class RestaurantRepository(ApplicationDbContext context) : IRestaurantRepository
{
    public async Task<List<Restaurant>> GetAllRestaurantsAsync()
    {
        return await context.Restaurants.Include(z=>z.Dishes).ToListAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id)
    {
        return await context.Restaurants.Include(z=>z.Dishes).FirstOrDefaultAsync(z=>z.Id == id);
    }

    public async Task<Guid> AddRestaurantAsync(Restaurant restaurant)
    {
        await context.Restaurants.AddAsync(restaurant);
        
        await context.SaveChangesAsync();

        return restaurant.Id;
    }

    public async Task<bool> DeleteRestaurantAsync(Restaurant restaurant)
    {
        context.Remove(restaurant);
        
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> SaveChangesAsync()
    {
        await context.SaveChangesAsync();
        return true;
    }
}