using HotelGaremo.Application.Common;
using HotelGaremo.Application.Features.Users.CreateUser;
using HotelGaremo.Application.Features.Users.LogIn;
using HotelGaremo.Application.Features.Users.VerifyUser;
using HotelGaremo.Application.requests;
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
        return Ok(new ApiResponse<CreateUserResponse>
        {
            StatusCode = 200,
            Message = "მომხმარებელი წარმატებით დარეგისტრირდა.",
            Data = response
        });
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var response = await mediator.Send(command);
        _logger.LogInformation("User logged in: {Email}", command.Email);
        return Ok(new ApiResponse<LoginResponse>
        {
            StatusCode = 200,
            Message = "წარმატებით შეხვედით სისტემაში.",
            Data = response
        });
    }
    [HttpPost("Verify-User/{email}")]
    public async Task<IActionResult> VerifyUser(
    [FromRoute] string email,
    [FromBody] VerifyUserRequest request)
    {
        var command = new VerifyUserCommand(email, request.VerificationCode);
        var response = await mediator.Send(command);
        _logger.LogInformation("User verified: {Email}", email);
        return Ok(new ApiResponse<VerifyUserResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }
}
