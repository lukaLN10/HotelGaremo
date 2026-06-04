using HotelGaremo.Domain.Enums;
using MediatR;

namespace HotelGaremo.Application.Features.Users.ChangeUserRole;

public record ChangeUserRoleCommand(int UserId, UserRoles Role) : IRequest<ChangeUserRoleResponse>;
