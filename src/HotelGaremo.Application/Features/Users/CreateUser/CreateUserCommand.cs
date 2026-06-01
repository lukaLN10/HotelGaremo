using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.CreateUser;

public record CreateUserCommand(
    string Name,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string Password,
    string PhoneNumber) : IRequest <CreateUserResponse>;

