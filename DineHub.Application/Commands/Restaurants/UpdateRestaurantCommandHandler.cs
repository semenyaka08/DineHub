using AutoMapper;
using DineHub.Domain.Entities;
using DineHub.Domain.Enums;
using DineHub.Domain.Exceptions;
using DineHub.Domain.Interfaces;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Restaurants;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with id: {Id}", request.Id);
        
        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, RecourseOperation.Update))
            throw new ForbiddenException("This operation is forbidden for you");
        
        mapper.Map(request, restaurant);
        await restaurantRepository.SaveChangesAsync();
    }
}