using DineHub.Application.Commands.Restaurants;
using DineHub.Application.Queries.Restaurants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DineHub.Api.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());

        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetRestaurantByIdQuery(id));

        if (result == null)
            return NotFound("restaurant was not found!");

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand request)
    {
        Guid id = await mediator.Send(request);

        return CreatedAtAction(nameof(GetRestaurantById), new {id}, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurantById(Guid id)
    {
        var result = await mediator.Send(new DeleteRestaurantCommand(id));

        if (result)
            return NoContent();

        return NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant(Guid id, [FromBody] UpdateRestaurantCommand request)
    {
        request.Id = id;
        
        var isUpdated = await mediator.Send(request);

        if (isUpdated)
            return NoContent();

        return NotFound();
    }
}