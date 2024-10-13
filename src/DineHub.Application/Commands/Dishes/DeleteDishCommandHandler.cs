using DineHub.Domain.Entities;
using DineHub.Domain.Exceptions;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Dishes;

public class DeleteDishCommandHandler(ILogger<DeleteDishCommandHandler> logger, IRestaurantRepository restaurantRepository, IDishRepository dishRepository) : IRequestHandler<DeleteDishCommand>
{
    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing dish from restaurant with id: {restaurantId} and dishId: {dishId}", request.RestaurantId, request.Id);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(z=>z.Id == request.Id);

        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.Id.ToString());

        await dishRepository.DeleteDishAsync(restaurant, dish);
    }
}