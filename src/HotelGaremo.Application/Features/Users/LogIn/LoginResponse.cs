using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.LogIn;

public record LoginResponse(int UserId, string Name, string LastName, string Role, string Token);