using DineHub.Application.Commands.Dishes;
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

        return Created();
    }
}