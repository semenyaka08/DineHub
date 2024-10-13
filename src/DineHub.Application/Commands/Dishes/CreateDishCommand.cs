using MediatR;

namespace DineHub.Application.Commands.Dishes;

public class CreateDishCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int KiloCalories { get; set; }

    public Guid RestaurantId { get; set; }
}