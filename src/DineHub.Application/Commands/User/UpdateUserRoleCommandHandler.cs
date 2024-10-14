using DineHub.Application.User;
using DineHub.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.User;

public class UpdateUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger, IUserContext userContext, RoleManager<IdentityRole> roleManager, UserManager<Domain.Entities.User> userManager) : IRequestHandler<UpdateUserRoleCommand>
{
    public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        
        logger.LogInformation("Updating the role: {Role} of the user with the id: {Id}", request.Role, currentUser.Id);

        var user = await userManager.FindByIdAsync(currentUser.Id);
        
        if (user is null)
            throw new NotFoundException(nameof(Domain.Entities.User), currentUser.Id);

        var role = await roleManager.FindByNameAsync(request.Role);
        
        if (role is null)
            throw new NotFoundException(nameof(IdentityRole), request.Role);
        
        await userManager.AddToRoleAsync(user, role.Name!);
    }
}