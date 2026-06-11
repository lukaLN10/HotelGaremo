using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Application.Features.Users.LogIn;
using HotelGaremo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Users.LogIn;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IDataContext _db;
    private readonly IJwtService _jwtService;

    public LoginHandler(IDataContext db, IJwtService jwtService)
    {
        _db = db;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new ValidationException(new List<ValidationFailure>
    {
        new ValidationFailure("Credentials", "მეილი ან პაროლი არასწორია.")
    });

        if (!user.IsActive)
            throw new BadRequestException("ანგარიში დეაქტივირებულია.");

        if (!user.IsVerified)
            throw new ValidationException(new List<ValidationFailure>
    {
        new ValidationFailure("Email", "ანგარიში ვერიფიცირებული არ არის.")
    });

        var token = _jwtService.GenerateToken(user);
        return new LoginResponse(user.Id, user.Name, user.LastName, user.Role.ToString(), token);


    }
}