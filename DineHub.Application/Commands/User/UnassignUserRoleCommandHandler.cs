using DineHub.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.User;

public class UnassignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger, UserManager<Domain.Entities.User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
{
    public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user with email: {email} role: {role}", request.UserEmail, request.Role);

        var user = await userManager.FindByEmailAsync(request.UserEmail);

        if (user is null)
            throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.Role);

        if (role is null)
            throw new NotFoundException(nameof(IdentityRole), request.Role);

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}