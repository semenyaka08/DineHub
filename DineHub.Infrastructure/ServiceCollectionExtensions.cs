using DineHub.Domain.Constants;
using DineHub.Domain.Entities;
using DineHub.Domain.Interfaces;
using DineHub.Domain.RepositoryContracts;
using DineHub.Infrastructure.Authorization;
using DineHub.Infrastructure.Authorization.Requirements;
using DineHub.Infrastructure.Repositories;
using DineHub.Infrastructure.Seeders;
using DineHub.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
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
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipleFactory>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        serviceCollection.AddScoped<IRestaurantRepository, RestaurantRepository>();
        serviceCollection.AddScoped<IDishRepository, DishRepository>();
        serviceCollection.AddScoped<IDataSeeder, DataSeeder>();
        serviceCollection.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        
        serviceCollection.AddAuthorizationBuilder()
            .AddPolicy(ApplicationPolicies.MinimumAgeAndNationalityPolicy,
                builder => builder.AddRequirements(new MinimumAgeRequirement(18), new NationalityRequirement()));

        serviceCollection.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        serviceCollection.AddScoped<IAuthorizationHandler, NationalityRequirementHandler>();
    }
}