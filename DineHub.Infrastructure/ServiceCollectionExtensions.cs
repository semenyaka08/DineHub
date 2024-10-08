using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using DineHub.Infrastructure.Repositories;
using DineHub.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DineHub.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging();
        });

        serviceCollection.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        serviceCollection.AddScoped<IRestaurantRepository, RestaurantRepository>();
        serviceCollection.AddScoped<IDishRepository, DishRepository>();
        serviceCollection.AddScoped<IDataSeeder, DataSeeder>();
    }
}