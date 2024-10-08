using DineHub.Domain.Entities;
using DineHub.Domain.Enums;
using DineHub.Domain.Exceptions;
using DineHub.Domain.Interfaces;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Restaurants;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting a repository with id: {Id}", request.Id);

        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, RecourseOperation.Delete))
            throw new ForbiddenException("This operation is forbidden for you");
        
        await restaurantRepository.DeleteRestaurantAsync(restaurant);
    }
}