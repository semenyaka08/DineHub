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
            new IdentityRole(ApplicationRoles.User)
            {
                NormalizedName = ApplicationRoles.User.ToUpper()
            },
            new IdentityRole(ApplicationRoles.Admin)
            {
                
                NormalizedName = ApplicationRoles.Admin.ToUpper()
            },
            new IdentityRole(ApplicationRoles.Owner)
            {
                
                NormalizedName = ApplicationRoles.Owner.ToUpper()
            },
        ];

        return roles;
    }
}