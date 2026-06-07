using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.GetAllBookings;

public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, List<GetAllBookingsResponse>>
{
    private readonly IDataContext _db;

    public GetAllBookingsHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetAllBookingsResponse>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Bookings
            .Include(x => x.Cottage)
            .Include(x => x.User)
            .Select(x => new GetAllBookingsResponse(
                x.Id,
                x.BookingNumber,
                x.Cottage.CottageName,
                x.User.Name,
                x.User.LastName,
                x.CheckIn,
                x.CheckOut,
                x.GuestCount,
                x.TotalPrice,
                x.PaymentType,
                x.BookingStatus))
            .ToListAsync(cancellationToken);
    }
}
