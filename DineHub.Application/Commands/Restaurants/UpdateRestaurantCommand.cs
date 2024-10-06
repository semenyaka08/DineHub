using MediatR;

namespace DineHub.Application.Commands.Restaurants;

public class UpdateRestaurantCommand : IRequest
{
    public Guid Id { get; set; } // Id of the restaurant we are trying to update
    
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }
    
    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;
}