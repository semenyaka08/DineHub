using DineHub.Application.Services;
using DineHub.Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DineHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        
        serviceCollection.AddScoped<IRestaurantService, RestaurantService>();

        serviceCollection.AddAutoMapper(applicationAssembly);

        serviceCollection.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();
    }
}