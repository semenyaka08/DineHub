using DineHub.Domain.Entities;
using DineHub.Domain.Enums;
using DineHub.Domain.Exceptions;
using DineHub.Domain.Interfaces;
using DineHub.Domain.RepositoryContracts;
using DineHub.Domain.Storage;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Restaurants;

public class UpdateRestaurantLogoCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository, IRestaurantAuthorizationService restaurantAuthorizationService, IBlobStorageService blobStorageService) : IRequestHandler<UpdateRestaurantLogoCommand>
{
    public async Task Handle(UpdateRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating the logo of the restaurant with id: {Id}", request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, RecourseOperation.Update))
            throw new ForbiddenException("This operation is forbidden for you!");

        var logoUrl = await blobStorageService.UploadLogoAsync(request.File, request.FileName);

        restaurant.LogoUrl = logoUrl;

        await restaurantRepository.SaveChangesAsync();
    }
}