using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Users.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<GetUsersResponse>>
{
    private readonly IDataContext _db;

    public GetUsersHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _db.Users
            .Select(u => new GetUsersResponse(
                u.Id,
                u.Name,
                u.LastName,
                u.Email,
                u.PhoneNumber,
                u.DateOfBirth,
                u.Role,
                u.IsVerified,
                u.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
