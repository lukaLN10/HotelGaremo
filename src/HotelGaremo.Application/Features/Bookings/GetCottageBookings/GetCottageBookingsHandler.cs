using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.GetCottageBookings;

public class GetCottageBookingsHandler : IRequestHandler<GetCottageBookingsQuery, List<GetCottageBookingsResponse>>
{
    private readonly IDataContext _db;

    public GetCottageBookingsHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetCottageBookingsResponse>> Handle(GetCottageBookingsQuery request, CancellationToken cancellationToken)
    {
        var cottageExists = await _db.Cottages
            .AnyAsync(x => x.Id == request.CottageId, cancellationToken);

        if (!cottageExists)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        return await _db.Bookings
            .Include(x => x.User)
            .Where(x => x.CottageId == request.CottageId)
            .Select(x => new GetCottageBookingsResponse(
                x.Id,
                x.BookingNumber,
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
