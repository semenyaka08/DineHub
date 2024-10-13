using MediatR;

namespace DineHub.Application.Commands.User;

public class UnassignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}