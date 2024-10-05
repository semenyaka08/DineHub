using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DineHub.Infrastructure.Repositories;

public class RestaurantRepository(ApplicationDbContext context) : IRestaurantRepository
{
    public async Task<List<Restaurant>> GetAllRestaurants()
    {
        return await context.Restaurants.Include(z=>z.Dishes).ToListAsync();
    }

    public async Task<Restaurant?> GetById(Guid id)
    {
        return await context.Restaurants.Include(z=>z.Dishes).FirstOrDefaultAsync(z=>z.Id == id);
    }

    public async Task<Guid> AddRestaurant(Restaurant restaurant)
    {
        await context.Restaurants.AddAsync(restaurant);
        
        await context.SaveChangesAsync();

        return restaurant.Id;
    }

    public async Task<bool> DeleteRestaurant(Restaurant restaurant)
    {
        context.Remove(restaurant);
        
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> SaveChanges()
    {
        await context.SaveChangesAsync();
        return true;
    }
}