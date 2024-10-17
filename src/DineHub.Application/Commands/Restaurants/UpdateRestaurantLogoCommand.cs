using MediatR;

namespace DineHub.Application.Commands.Restaurants;

public class UpdateRestaurantLogoCommand : IRequest
{
    public Guid RestaurantId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public Stream File { get; set; } = default!;
}