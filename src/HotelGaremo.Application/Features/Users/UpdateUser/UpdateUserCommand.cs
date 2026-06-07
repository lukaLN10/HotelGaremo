using MediatR;

namespace HotelGaremo.Application.Features.Users.UpdateUser;

public record UpdateUserCommand(
       string Name,
       string LastName,
       string PhoneNumber,
       DateTime DateOfBirth) :
       IRequest<UpdateUserResponse>;
