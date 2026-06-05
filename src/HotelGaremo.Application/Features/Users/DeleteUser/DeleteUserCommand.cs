using MediatR;

namespace HotelGaremo.Application.Features.Users.DeleteUser;

public record DeleteUserCommand() : IRequest<DeleteUserResponse>;
