namespace DineHub.Application.User;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles, DateTime? BirthDate, string? Nationality)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}