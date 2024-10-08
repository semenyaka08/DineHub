using MediatR;

namespace DineHub.Application.Commands.User;

public class AssignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}