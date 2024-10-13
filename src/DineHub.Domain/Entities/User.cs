using Microsoft.AspNetCore.Identity;

namespace DineHub.Domain.Entities;

public class User : IdentityUser
{
    public DateTime? BirthDate { get; set; }

    public string? Nationality { get; set; }

    public ICollection<Restaurant> Restaurants { get; set; } = [];
}