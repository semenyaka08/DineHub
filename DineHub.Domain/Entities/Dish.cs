using System.Security.AccessControl;

namespace DineHub.Domain.Entities;

public class Dish
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int KiloCalories { get; set; }
    
    public Restaurant Restaurant { get; set; } = null!;

    public Guid RestaurantId { get; set; }
}