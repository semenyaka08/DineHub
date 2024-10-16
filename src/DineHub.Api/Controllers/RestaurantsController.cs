using DineHub.Application.Commands.Restaurants;
using DineHub.Application.Queries.Restaurants;
using DineHub.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DineHub.Api.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants([FromQuery] GetAllRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);

        return Ok(restaurants);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetRestaurantByIdQuery(id));

        return Ok(result);
    }
    
    [Authorize(Roles = ApplicationRoles.Owner)]
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand request)
    {
        Guid id = await mediator.Send(request);

        return CreatedAtAction(nameof(GetRestaurantById), new {id}, null);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurantById(Guid id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));

        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id}/info")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] Guid id, [FromBody] UpdateRestaurantCommand request)
    {
        request.Id = id;
        
        await mediator.Send(request);
        
        return NoContent();
    }
    
    
    [HttpPatch("{id}/logo")]
    public async Task<IActionResult> UpdateRestaurantLogo([FromRoute] Guid id, IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        var command = new UpdateRestaurantLogoCommand
        {
            RestaurantId = id,
            FileName = file.FileName,
            File = stream
        };

        await mediator.Send(command);

        return NoContent();

    }
}