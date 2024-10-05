namespace DineHub.Application.Dtos.DishDtos;

public class DishDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int KiloCalories { get; set; }
}