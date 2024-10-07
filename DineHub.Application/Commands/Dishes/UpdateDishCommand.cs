using MediatR;

namespace DineHub.Application.Commands.Dishes;

public class UpdateDishCommand : IRequest
{
    public Guid DishId { get; set; }

    public Guid RestaurantId { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int KiloCalories { get; set; }
}