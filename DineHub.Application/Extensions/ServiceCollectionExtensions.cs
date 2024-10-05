using DineHub.Application.Services;
using DineHub.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DineHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRestaurantService, RestaurantService>();

        serviceCollection.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }
}