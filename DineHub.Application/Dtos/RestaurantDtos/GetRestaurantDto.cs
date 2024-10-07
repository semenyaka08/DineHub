using DineHub.Application.Dtos.DishDtos;

namespace DineHub.Application.Dtos.RestaurantDtos;

public class GetRestaurantDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public List<DishDto> Dishes { get; set; } = [];
}