using HotelGaremo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.ChangeUserRole;

public record ChangeUserRoleResponse(int id, UserRoles role)
{
}
