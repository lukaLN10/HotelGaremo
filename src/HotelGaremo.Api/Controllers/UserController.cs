using HotelGaremo.Application.Features.Users.ChangeUserRole;
using HotelGaremo.Application.Features.Users.CreateUser;
using HotelGaremo.Application.Features.Users.RecoveryPassword;
using HotelGaremo.Application.Features.Users.SendPasswordRecoveryCode;
using HotelGaremo.Application.Features.Users.VerifyUser;
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
    [HttpPut("Verify-User")]
    public async Task<IActionResult> VerifyUser(VerifyUserCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }
    [HttpPut("Change-User-Role")]
    public async Task<IActionResult> ChangeUserRole(ChangeUserRoleCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }
    [HttpPost("Send-Password-Recovery-Code")]
    public async Task<IActionResult> SendPasswordRecoveryCode(SendPasswordRecoveryCodeCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }
    [HttpPost("Recovery-Password")]
    public async Task<IActionResult> RecoveryPassword(RecoveryPasswordCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }


}

