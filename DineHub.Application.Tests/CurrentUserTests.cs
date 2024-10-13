using DineHub.Application.User;
using DineHub.Domain.Constants;
using FluentAssertions;

namespace DineHub.Application.Tests;

public class CurrentUserTests
{
    private readonly CurrentUser _user;

    public CurrentUserTests()
    {
        _user = new CurrentUser("1", "someEmail@gmail.com", [ApplicationRoles.User, ApplicationRoles.Admin], null, null);
    }
    
    [Fact]
    public void IsInRole_MatchingRole_ReturnTrue()
    {
        //arrange
        
        //act
        var inRoleResult = _user.IsInRole(ApplicationRoles.User);
        
        //assert
        inRoleResult.Should().BeTrue();
    }
    
    [Fact]
    public void IsInRole_NoMatchingRole_ReturnFalse()
    {
        //arrange
        
        //act
        var inRoleResult = _user.IsInRole(ApplicationRoles.Owner);
        
        //assert
        inRoleResult.Should().BeFalse();
    }
}