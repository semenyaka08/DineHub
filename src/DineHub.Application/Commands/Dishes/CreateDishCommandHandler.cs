using AutoMapper;
using DineHub.Application.User;
using DineHub.Domain.Entities;
using DineHub.Domain.Exceptions;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Dishes;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository, IDishRepository dishRepository, IUserContext userContext) : IRequestHandler<CreateDishCommand, Guid>
{
    public async Task<Guid> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new dish: {@Dish}", request);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var user = userContext.GetCurrentUser();

        if (user.Id != restaurant.UserId)
            throw new ForbiddenException("This operation is forbidden for you!");
        
        var dishEntity = mapper.Map<Dish>(request);

        Guid id = await dishRepository.AddDishAsync(dishEntity);

        return id;
    }
}