using DineHub.Application.Commands.User;
using DineHub.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DineHub.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPatch("updateUserDetails")]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }

    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPatch("assignUserRole")]
    public async Task<IActionResult> AssignUserRole([FromBody] AssignUserRoleCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }
    
    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPatch("unassignUserRole")]
    public async Task<IActionResult> UnassignUserRole([FromBody] UnassignUserRoleCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }

    [Authorize]
    [HttpPatch("updateUserRole")]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleCommand command)
    {
        if (command.Role.ToLower() == "admin")
            return BadRequest("This role is forbidden for you");
        
        await mediator.Send(command);

        return NoContent();
    }
}