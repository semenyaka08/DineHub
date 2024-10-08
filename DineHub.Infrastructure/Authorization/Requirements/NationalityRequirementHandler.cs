using DineHub.Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace DineHub.Infrastructure.Authorization.Requirements;

public class NationalityRequirementHandler(ILogger<NationalityRequirementHandler> logger, IUserContext userContext) : AuthorizationHandler<NationalityRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NationalityRequirement requirement)
    {
        var user = userContext.GetCurrentUser();
        
        if(user is null)
        {
            logger.LogWarning("you have to be authorized");
            context.Fail();
            return Task.CompletedTask;
        }
        
        logger.LogInformation("Checking if user with email: {email} has non null Nationality field", user.Email);

        if (user.Nationality != null)
        {
            logger.LogInformation("Nationality field is not null");
            context.Succeed(requirement);
        }
        else
            context.Fail();

        return Task.CompletedTask;
    }
}