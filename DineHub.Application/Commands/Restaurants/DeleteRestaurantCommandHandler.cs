using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Restaurants;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting a repository with id: {request.Id}");

        var restaurant = await restaurantRepository.GetById(request.Id);

        if (restaurant == null)
            return false;

        return await restaurantRepository.DeleteRestaurant(restaurant);
    }
}