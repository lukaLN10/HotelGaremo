using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Users.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IDataContext _db;

    public GetUserByIdHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _db.Users
            .Where(u => u.Id == request.Id && u.IsActive)
            .Select(u => new GetUserByIdResponse(
                u.Id,
                u.Name,
                u.LastName,
                u.Email,
                u.PhoneNumber,
                u.DateOfBirth,
                u.Role,
                u.IsVerified,
                u.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            throw new ValidationException(new List<ValidationFailure>
            {
                new("Id", "მომხმარებელი ვერ მოიძებნა.")
            });

        return user;
    }
}
