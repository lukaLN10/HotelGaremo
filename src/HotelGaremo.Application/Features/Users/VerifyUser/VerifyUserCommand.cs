using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.VerifyUser;

public record VerifyUserCommand(string Email, int VerificationCode) : IRequest<VerifyUserResponse>;
