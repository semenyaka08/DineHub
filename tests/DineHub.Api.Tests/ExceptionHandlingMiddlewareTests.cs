using DineHub.Api.Middlewares;
using DineHub.Domain.Entities;
using DineHub.Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DineHub.Api.Tests;

public class ExceptionHandlingMiddlewareTests
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly ExceptionHandlingMiddleware _middleware;
    private readonly DefaultHttpContext _context;
    
    public ExceptionHandlingMiddlewareTests()
    {
        _logger = Substitute.For<ILogger<ExceptionHandlingMiddleware>>();
        _context = new DefaultHttpContext();
        _middleware = new ExceptionHandlingMiddleware(_logger);
    }

    [Fact]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
    {
        //arrange
        var nextDelegate = Substitute.For<RequestDelegate>();
        
        //act
        await _middleware.InvokeAsync(_context, nextDelegate);

        //assert
        await nextDelegate.Received(1).Invoke(_context);
    }
    
    [Fact]
    public async Task InvokeAsync_WhereForbiddenExceptionThrown_ShouldSet403StatusCode()
    {
        //arrange
        var exception = new ForbiddenException("exception");
        
        //act
        await _middleware.InvokeAsync(_context, _ => throw exception);

        //assert
        _context.Response.StatusCode.Should().Be(403);
    }
    
    [Fact]
    public async Task InvokeAsync_WhenUnauthorizedAccessExceptionExceptionThrown_ShouldSet401StatusCode()
    {
        //arrange
        var exception = new UnauthorizedAccessException("exception");
        
        //act
        await _middleware.InvokeAsync(_context, _ => throw exception);

        //assert
        _context.Response.StatusCode.Should().Be(401);
    }
    
    [Fact]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSet404StatusCode()
    {
        //arrange
        var exception = new NotFoundException(nameof(Restaurant), "2");
        
        //act
        await _middleware.InvokeAsync(_context, _ => throw exception);

        //assert
        _context.Response.StatusCode.Should().Be(404);
    }
    
    [Fact]
    public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSet500StatusCode()
    {
        //arrange
        var exception = new Exception();
        
        //act
        await _middleware.InvokeAsync(_context, _ => throw exception);

        //assert
        _context.Response.StatusCode.Should().Be(500);
    }
}