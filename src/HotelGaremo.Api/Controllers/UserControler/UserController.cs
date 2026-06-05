using HotelGaremo.Application.Common;
using HotelGaremo.Application.Features.Users.ChangeUserRole;
using HotelGaremo.Application.Features.Users.CreateUser;
using HotelGaremo.Application.Features.Users.DeleteUser;
using HotelGaremo.Application.Features.Users.DeleteUserByAdmin;
using HotelGaremo.Application.Features.Users.ForgotPassword;
using HotelGaremo.Application.Features.Users.GetUserById;
using HotelGaremo.Application.Features.Users.GetUsers;
using HotelGaremo.Application.Features.Users.LogIn;
using HotelGaremo.Application.Features.Users.ResetPassword;
using HotelGaremo.Application.Features.Users.UpdateUser;
using HotelGaremo.Application.Features.Users.VerifyUser;
using HotelGaremo.Application.requests;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("Get-User/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var response = await mediator.Send(new GetUserByIdQuery(id));
        return Ok(new ApiResponse<GetUserByIdResponse>
        {
            StatusCode = 200,
            Message = "მომხმარებელი წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpGet("Get-Users")]
    public async Task<IActionResult> GetUsers()
    {
        var response = await mediator.Send(new GetUsersQuery());
        return Ok(new ApiResponse<List<GetUsersResponse>>
        {
            StatusCode = 200,
            Message = "მომხმარებლები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpPost("Create-User")]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        var response = await mediator.Send(command);
        _logger.LogInformation("User created with ID: {UserId}", response.id);
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

    [HttpPost("Forgot-Password/{email}")]
    public async Task<IActionResult> ForgotPassword([FromRoute] string email)
    {
        var command = new ForgotPasswordCommand(email);
        var response = await mediator.Send(command);
        return Ok(new ApiResponse<ForgotPasswordResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpPost("Reset-Password/{email}")]
    public async Task<IActionResult> ResetPassword(
        [FromRoute] string email,
        [FromBody] ResetPasswordRequest request)
    {
        var command = new ResetPasswordCommand(email, request.Code, request.NewPassword);
        var response = await mediator.Send(command);
        return Ok(new ApiResponse<ResetPasswordResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("Change-Role/{userId}")]
    public async Task<IActionResult> ChangeUserRole(
        [FromRoute] int userId,
        [FromBody] ChangeUserRoleRequest request)
    {
        var command = new ChangeUserRoleCommand(userId, request.Role);
        var response = await mediator.Send(command);
        return Ok(new ApiResponse<ChangeUserRoleResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [Authorize]
    [HttpPut("Update-User")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(new ApiResponse<UpdateUserResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [Authorize]
    [HttpDelete("Delete-User")]
    public async Task<IActionResult> DeleteUser()
    {
        var response = await mediator.Send(new DeleteUserCommand());
        _logger.LogInformation("User deactivated.");
        return Ok(new ApiResponse<DeleteUserResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpDelete("Delete-User-By-Admin/{userId}")]
    public async Task<IActionResult> DeleteUserByAdmin(int userId)
    {
        var response = await mediator.Send(new DeleteUserByAdminCommand(userId));
        _logger.LogInformation("User deactivated by admin. UserId: {UserId}", userId);
        return Ok(new ApiResponse<DeleteUserByAdminResponse>
        {
            StatusCode = 200,
            Message = response.message,
            Data = response
        });
    }
}
