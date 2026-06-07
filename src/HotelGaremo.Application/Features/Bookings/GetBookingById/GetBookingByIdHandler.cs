using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.GetBookingById;

public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdQuery, GetBookingByIdResponse>
{
    private readonly IDataContext _db;

    public GetBookingByIdHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<GetBookingByIdResponse> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        var booking = await _db.Bookings
            .Include(x => x.Cottage)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

        if (booking == null)
            throw new BadRequestException("ჯავშანი ვერ მოიძებნა.");

        return new GetBookingByIdResponse(
            booking.Id,
            booking.BookingNumber,
            booking.GroupBookingNumber,
            booking.Cottage.CottageName,
            booking.User.Name,
            booking.User.LastName,
            booking.User.Email,
            booking.CheckIn,
            booking.CheckOut,
            booking.GuestCount,
            booking.TotalPrice,
            booking.PaymentType,
            booking.BookingStatus);
    }
}
