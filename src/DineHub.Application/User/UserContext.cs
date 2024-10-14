using System.Security.Claims;
using DineHub.Domain.Entities;
using DineHub.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DineHub.Application.User;

public interface IUserContext
{
    public CurrentUser GetCurrentUser();
}

public class UserContext(IHttpContextAccessor contextAccessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        var user = contextAccessor.HttpContext?.User;
        
        if (user is null)
            throw new InvalidOperationException("User context is not present");
        
        var userId = user.FindFirst(z=>z.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(z=>z.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(z => z.Type == ClaimTypes.Role).Select(z=>z.Value);
        var birthDateString = user.FindFirst(z=>z.Type == "BirthDate")?.Value;
        var nationality = user.FindFirst(z=>z.Type == "Nationality")?.Value;
        DateTime? birthDate = DateTime.TryParse(birthDateString, out var parsedDate) ? parsedDate : null;
        
        return new CurrentUser(userId, email, roles, birthDate, nationality);
    }
}