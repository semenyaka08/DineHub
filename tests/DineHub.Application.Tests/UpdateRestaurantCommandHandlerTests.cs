using AutoMapper;
using DineHub.Application.Commands.Restaurants;
using DineHub.Domain.Entities;
using DineHub.Domain.Enums;
using DineHub.Domain.Exceptions;
using DineHub.Domain.Interfaces;
using DineHub.Domain.RepositoryContracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DineHub.Application.Tests;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IRestaurantAuthorizationService _authorizationService;
    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        _logger = Substitute.For<ILogger<UpdateRestaurantCommandHandler>>();
        _mapper = Substitute.For<IMapper>();
        _restaurantRepository = Substitute.For<IRestaurantRepository>();
        _authorizationService = Substitute.For<IRestaurantAuthorizationService>();
        _handler = new UpdateRestaurantCommandHandler(_logger, _restaurantRepository, _mapper, _authorizationService);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldUpdateRestaurant()
    {
        //arrange
        var request = new UpdateRestaurantCommand {Id = Guid.NewGuid()};
        
        var restaurant = new Restaurant{Id = request.Id};

        _restaurantRepository.GetByIdAsync(restaurant.Id).Returns(restaurant);

        _authorizationService.Authorize(restaurant, RecourseOperation.Update).Returns(true);
        //act

        await _handler.Handle(request, CancellationToken.None);

        //assert
        _mapper.Received(1).Map(request, restaurant);
        await _restaurantRepository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handle_WithNotExistRestaurant_ShouldReturnNotFoundException()
    {
        //arrange
        var request = new UpdateRestaurantCommand{Id = Guid.NewGuid()};
        
        _restaurantRepository.GetByIdAsync(request.Id).ReturnsNull();
        
        //act

        var func = () => _handler.Handle(request, CancellationToken.None);

        //assert

        await func.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task Handle_WithNotPermission_ShouldReturnForbiddenException()
    {
        //arrange
        var request = new UpdateRestaurantCommand{Id = Guid.NewGuid()};

        var restaurant = new Restaurant { Id = request.Id };
        
        _restaurantRepository.GetByIdAsync(request.Id).Returns(restaurant);
        
        _authorizationService.Authorize(restaurant, RecourseOperation.Update).Returns(false);
        
        //act

        var func = () => _handler.Handle(request, CancellationToken.None);

        //assert

        await func.Should().ThrowAsync<ForbiddenException>()
            .WithMessage("This operation is forbidden for you");
    }
}