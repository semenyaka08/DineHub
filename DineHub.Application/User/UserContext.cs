using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DineHub.Application.User;

public interface IUserContext
{
    public CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor contextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = contextAccessor.HttpContext?.User;

        if (user is null)
            throw new InvalidOperationException("User context is not present");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
            return null;

        var userId = user.FindFirst(z=>z.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(z=>z.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(z => z.Type == ClaimTypes.Role).Select(z=>z.Value);

        return new CurrentUser(userId, email, roles);
    }
}