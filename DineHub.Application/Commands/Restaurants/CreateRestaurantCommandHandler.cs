using AutoMapper;
using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Restaurants;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<CreateRestaurantCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a restaurant: {@Restaurant}", request);
        
        var restaurant = mapper.Map<Restaurant>(request);

        Guid id = await restaurantRepository.AddRestaurantAsync(restaurant);

        return id;
    }
}