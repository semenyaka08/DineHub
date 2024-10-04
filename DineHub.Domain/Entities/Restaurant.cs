namespace DineHub.Domain.Entities;

public class Restaurant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public Address? Address { get; set; }

    public ICollection<Dish> Dishes { get; set; } = [];
}