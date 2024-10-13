using System.Security.Claims;
using DineHub.Application.User;
using DineHub.Domain.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace DineHub.Application.Tests;

public class UserContextTests
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserContext _userContext;
    
    public UserContextTests()
    {
        _contextAccessor = Substitute.For<IHttpContextAccessor>();
        _userContext = new UserContext(_contextAccessor);
    }
    
    [Fact]
    public void GetCurrentUser_WhenUserIsAuthenticated_ShouldReturnCurrentUser()
    {
        //arrange
        DateTime birthDate = new DateTime(2005, 8, 8);
        
        var claimsList = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, "2"),
            new(ClaimTypes.Email, "email@gmail.com"),
            new(ClaimTypes.Role, ApplicationRoles.User),
            new(ClaimTypes.Role, ApplicationRoles.Admin),
            new("BirthDate", birthDate.ToString("yyyy-MM-dd")),
            new("Nationality", "German")
        };
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(claimsList, "test"));

        _contextAccessor.HttpContext.Returns(new DefaultHttpContext
        {
            User = user
        });

        //act
        var currentUser = _userContext.GetCurrentUser();
        
        //assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("2");
        currentUser.Email.Should().Be("email@gmail.com");
        currentUser.Roles.Should().ContainInOrder(ApplicationRoles.User, ApplicationRoles.Admin);
        currentUser.BirthDate.Should().Be(birthDate);
        currentUser.Nationality.Should().Be("German");
    }

    [Fact]
    public void GetCurrentUser_WhenUserIsNotAuthenticated_ShouldThrowInvalidOperationException()
    {
        //arrange
        _contextAccessor.HttpContext.Returns((HttpContext)null!);
        
        //act
        Action action = () => _userContext.GetCurrentUser();
        
        //assertion
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User context is not present");
    }
}