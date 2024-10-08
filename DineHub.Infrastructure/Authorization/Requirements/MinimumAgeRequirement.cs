using Microsoft.AspNetCore.Authorization;

namespace DineHub.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirement(int age) : IAuthorizationRequirement
{
    public int MinimalAge { get; } = age;
}