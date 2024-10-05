using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DineHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        serviceCollection.AddAutoMapper(applicationAssembly);

        serviceCollection.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();
    }
}