using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.Users.GetUserById;

public record GetUserByIdResponse(
    int Id,
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    UserRoles Role,
    bool IsVerified,
    DateTime CreatedAt);
