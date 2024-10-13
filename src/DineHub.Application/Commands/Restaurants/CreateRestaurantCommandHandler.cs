using AutoMapper;
using DineHub.Application.User;
using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Restaurants;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        if (user is null)
            throw new UnauthorizedAccessException("Try to authorize before adding the restaurant");
        
        logger.LogInformation("Creating a restaurant: {@Restaurant}", request);
        
        var restaurant = mapper.Map<Restaurant>(request);

        restaurant.UserId = user.Id;
        
        Guid id = await restaurantRepository.AddRestaurantAsync(restaurant);

        return id;
    }
}