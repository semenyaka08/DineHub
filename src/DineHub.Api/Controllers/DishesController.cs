using DineHub.Application.Commands.Dishes;
using DineHub.Application.Queries.Dishes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DineHub.Api.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] Guid restaurantId, [FromBody] CreateDishCommand request)
    {
        request.RestaurantId = restaurantId;

        var id = await mediator.Send(request);

        return CreatedAtAction(nameof(GetDishById), new {restaurantId, id}, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetDishes([FromRoute] Guid restaurantId)
    {
        var dishes = await mediator.Send(new GetAllDishesQuery(restaurantId));

        return Ok(dishes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDishById([FromRoute] Guid restaurantId, [FromRoute] Guid id)
    {
        var dish = await mediator.Send(new GetDishByIdQuery(id: id, restaurantId: restaurantId));

        return Ok(dish);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDish([FromRoute] Guid restaurantId, [FromRoute] Guid id)
    {
        await mediator.Send(new DeleteDishCommand(restaurantId: restaurantId, id: id));

        return NoContent();
    }

    [HttpPatch("{dishId}")]
    public async Task<IActionResult> UpdateDish([FromRoute] Guid restaurantId, [FromRoute] Guid dishId, [FromBody] UpdateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        command.DishId = dishId;

        await mediator.Send(command);

        return NoContent();
    }
}