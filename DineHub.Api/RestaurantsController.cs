using DineHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DineHub.Api;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await restaurantService.GetAllRestaurants();

        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById([FromRoute] Guid id)
    {
        var result = await restaurantService.GetById(id);

        if (result == null)
            return NotFound("restaurant was not found!");

        return Ok(result);
    }
}