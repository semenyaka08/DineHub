using DineHub.Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace DineHub.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger, IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var user = userContext.GetCurrentUser();
        
        if(user is null)
        {
            logger.LogWarning("you have to be authorized");
            context.Fail();
            return Task.CompletedTask;
        }
        
        if(user.BirthDate is null)
        {
            logger.LogWarning("BirthDate of User is null");
            context.Fail();
            return Task.CompletedTask;
        }

        logger.LogInformation("User: {Email}, dOb: {BirthDate}, Handling the MinimalAgeRequirement", user.Email, user.BirthDate);

        if (user.BirthDate.Value.AddYears(requirement.MinimalAge) < DateTime.Today)
        {
            logger.LogInformation("Success: User is older than {age}", requirement.MinimalAge);
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        
        return Task.CompletedTask;
    }
}