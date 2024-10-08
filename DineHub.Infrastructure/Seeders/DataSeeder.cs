using DineHub.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace DineHub.Infrastructure.Seeders;

public class DataSeeder(ApplicationDbContext context ) : IDataSeeder
{
    public async Task Seed()
    {
        if (await context.Database.CanConnectAsync())
        {
            if (!context.Roles.Any())
            {
                var roles = GetRoles();
                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = 
        [
            new IdentityRole(ApplicationRoles.User),
            new IdentityRole(ApplicationRoles.Admin),
            new IdentityRole(ApplicationRoles.Owner),
        ];

        return roles;
    }
}