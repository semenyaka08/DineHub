using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DineHub.Infrastructure.Repositories;

public class RestaurantRepository(ApplicationDbContext context) : IRestaurantRepository
{
    public async Task<(List<Restaurant>, int itemsCount)> GetAllMatchingRestaurantsAsync(string? searchString, int pageSize, int pageNumber)
    {
        var searchPhrase = searchString?.ToLower();

        var baseQuery = context.Restaurants
            .Where(z => searchPhrase == null
                        || z.Name.ToLower().Contains(searchPhrase)
                        || z.Description.ToLower().Contains(searchPhrase));

        int itemsCount = await baseQuery.CountAsync();

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageNumber)
            .ToListAsync();
        
        return (restaurants, itemsCount);
    }

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