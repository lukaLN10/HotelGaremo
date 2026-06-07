using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.GetMyBookings;

public class GetMyBookingsHandler : IRequestHandler<GetMyBookingsQuery, List<GetMyBookingsResponse>>
{
    private readonly IDataContext _db;

    public GetMyBookingsHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetMyBookingsResponse>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Bookings
            .Include(x => x.Cottage)
            .Where(x => x.UserId == request.UserId)
            .Select(x => new GetMyBookingsResponse(
                x.Id,
                x.BookingNumber,
                x.Cottage.CottageName,
                x.CheckIn,
                x.CheckOut,
                x.GuestCount,
                x.TotalPrice,
                x.PaymentType,
                x.BookingStatus))
            .ToListAsync(cancellationToken);
    }
}
