using System.Security.Claims;
using DineHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DineHub.Infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipleFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);
        
        if(user.Nationality != null)
            id.AddClaim(new Claim("Nationality", user.Nationality));
        
        if(user.BirthDate != null)
            id.AddClaim(new Claim("BirthDate", user.BirthDate.Value.ToString("yyyy MMMM dd")));

        return new ClaimsPrincipal(id);
    }
}