using HotelGaremo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.ChangeUserRole;

public record ChangeUserRoleCommand(string email, UserRoles role) : IRequest<ChangeUserRoleResponse>
{
}
