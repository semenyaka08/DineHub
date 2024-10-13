using AutoMapper;
using DineHub.Application.Commands.Restaurants;
using DineHub.Application.User;
using DineHub.Domain.Constants;
using DineHub.Domain.Entities;
using DineHub.Domain.RepositoryContracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DineHub.Application.Tests;

public class CreateRestaurantCommandHandlerTest
{
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUserContext _userContext;
    private readonly CreateRestaurantCommandHandler _handler;
    
    public CreateRestaurantCommandHandlerTest()
    {
        _logger = Substitute.For<ILogger<CreateRestaurantCommandHandler>>();
        _mapper = Substitute.For<IMapper>();
        _restaurantRepository = Substitute.For<IRestaurantRepository>();
        _userContext = Substitute.For<IUserContext>();
        _handler = new CreateRestaurantCommandHandler(_logger, _mapper, _restaurantRepository, _userContext);
    }

    [Fact]
    public async Task Handle_WhenUserIsNotAuthorized_ShouldThrowUnauthorizedAccessException()
    {
        //arrange
        var command = new CreateRestaurantCommand();
        _userContext.GetCurrentUser().ReturnsNull();
        //assert

        var action = async () => await _handler.Handle(command, CancellationToken.None);

        //act
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
    
    [Fact]
    public async Task Handle_WhenUserIsAuthorized_ShouldCreateRestaurant()
    {
        //arrange
        var user = new CurrentUser("1", "email@gmail.com", [], null, null);
        
        Guid restaurantId = Guid.NewGuid();
        
        var restaurant = new Restaurant();

        var request = new CreateRestaurantCommand();

        _mapper.Map<Restaurant>(request).Returns(restaurant);
        
        _userContext.GetCurrentUser().Returns(user);

        _restaurantRepository.AddRestaurantAsync(restaurant).Returns(restaurantId);

        //assert

        var result = await _handler.Handle(request, CancellationToken.None);

        //act
        result.Should().Be(restaurantId);

        restaurant.UserId.Should().Be(user.Id);
        
        await _restaurantRepository.Received(1).AddRestaurantAsync(restaurant);
    }
}