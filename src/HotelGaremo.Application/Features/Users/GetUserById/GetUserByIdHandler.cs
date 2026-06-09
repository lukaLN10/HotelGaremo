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
            .Include(u => u.Bookings)
            .Include(u => u.Reviews)
            .Where(u => u.Id == request.Id && u.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            throw new ValidationException(new List<ValidationFailure>
            {
                new("Id", "მომხმარებელი ვერ მოიძებნა.")
            });

        return new GetUserByIdResponse(
            user.Id,
            user.Name,
            user.LastName,
            user.Email,
            user.PhoneNumber,
            user.DateOfBirth,
            user.Role,
            user.IsVerified,
            user.CreatedAt,
            user.Bookings.Select(b => new UserBookingDto(
                b.BookingNumber,
                b.CheckIn,
                b.CheckOut,
                b.GuestCount,
                b.TotalPrice,
                b.PaymentType,
                b.BookingStatus)).ToList(),
            user.Reviews.Select(r => new UserReviewDto(
                r.Id,
                r.Comment,
                r.Rating)).ToList());
    }
}
