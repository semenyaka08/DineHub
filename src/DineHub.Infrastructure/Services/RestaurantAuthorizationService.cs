using DineHub.Application.User;
using DineHub.Domain.Constants;
using DineHub.Domain.Entities;
using DineHub.Domain.Enums;
using DineHub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace DineHub.Infrastructure.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext context) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, RecourseOperation operation)
    {
        var user = context.GetCurrentUser();
        
        if(user is null)
            throw new UnauthorizedAccessException("Try to authorize before adding the restaurant");
        
        logger.LogInformation("Authorizing user: {email}, to operation {operation} for restaurant {restaurantId}", user.Email, operation, restaurant.Id);

        if (operation == RecourseOperation.Read || operation == RecourseOperation.Create)
        {
            logger.LogInformation("Creating/Reading operation - successful authorization");
            return true;
        }

        if (operation == RecourseOperation.Delete && user.IsInRole(ApplicationRoles.Admin))
        {
            logger.LogInformation("Admin user, Delete operation - successful authorization");
            return true;
        }
        
        if ((operation == RecourseOperation.Delete || operation == RecourseOperation.Update) && user.Id == restaurant.UserId)
        {
            logger.LogInformation("Restaurant Owner - successful authorization");
            return true;
        }

        return false;
    }
}