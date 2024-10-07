using MediatR;

namespace DineHub.Application.Commands.User;

public class UpdateUserDetailsCommand : IRequest
{
    public DateTime? BirthDate { get; set; }

    public string? Nationality { get; set; }
}