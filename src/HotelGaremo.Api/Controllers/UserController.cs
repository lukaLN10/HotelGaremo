using HotelGaremo.Application.Features.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelGaremo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost ("Create-User")]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);

    } 
}

