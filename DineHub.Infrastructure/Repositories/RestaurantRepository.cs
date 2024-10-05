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
}