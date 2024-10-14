using MediatR;

namespace DineHub.Application.Commands.User;

public class UpdateUserRoleCommand : IRequest
{
    public string Role { get; set; } = string.Empty;
}