using System.Linq.Expressions;
using DineHub.Application.Common;
using DineHub.Domain.Entities;
using DineHub.Domain.Enums;
using DineHub.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DineHub.Infrastructure.Repositories;

public class RestaurantRepository(ApplicationDbContext context) : IRestaurantRepository
{
    public async Task<(List<Restaurant>, int itemsCount)> GetAllMatchingRestaurantsAsync(string? searchString, int pageSize, int pageNumber, string sortItem, SortOrder sortOrder)
    {
        var searchPhrase = searchString?.ToLower();
        var orderDictionary = new Dictionary<string, Expression<Func<Restaurant, object>>>()
        {
            {"Name", z=>z.Name},
            {"Category", z=>z.Category},
            {"Rating", z=>z.Rating}
        };
        
        var baseQuery = context.Restaurants
            .Where(z => searchPhrase == null
                        || z.Name.ToLower().Contains(searchPhrase)
                        || z.Description.ToLower().Contains(searchPhrase));

        int itemsCount = await baseQuery.CountAsync();

        var sortItemExpression = orderDictionary[sortItem];

        var orderedRestaurants = sortOrder == SortOrder.Ascending ?  await baseQuery.OrderBy(sortItemExpression).ToListAsync() : await baseQuery.OrderByDescending(sortItemExpression).ToListAsync();//await restaurants.OrderBy(sortItemExpression).ToListAsync();
        
        var restaurants = orderedRestaurants
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToList();
        
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