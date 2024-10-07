using DineHub.Application.User;
using DineHub.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.User;

public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,IUserContext context, IUserStore<Domain.Entities.User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = context.GetCurrentUser();
        
        logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

        if (dbUser is null)
            throw new NotFoundException(nameof(Domain.Entities.User), user!.Id);

        dbUser.Nationality = request.Nationality;
        dbUser.BirthDate = request.BirthDate;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}