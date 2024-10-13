using MediatR;

namespace DineHub.Application.Commands.Restaurants;

public class DeleteRestaurantCommand(Guid id) : IRequest
{
    public Guid Id { get;} = id;
}