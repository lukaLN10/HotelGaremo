using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.LogIn;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;

