using HotelGaremo.Application.Features.Users.CreateUser;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        this.mediator = mediator;
        _logger = logger;
    }

    [HttpPost("Create-User")]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        var response = await mediator.Send(command);
        _logger.LogInformation("მომხმარებელი დარეგისტრირდა: {FirstName} {LastName}",
            command.Name, command.LastName);
        return Ok(response);
    }
}
