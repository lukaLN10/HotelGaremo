using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.ChangeBookingStatus;

public class ChangeBookingStatusHandler : IRequestHandler<ChangeBookingStatusCommand, ChangeBookingStatusResponse>
{
    private readonly IDataContext _db;

    public ChangeBookingStatusHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<ChangeBookingStatusResponse> Handle(ChangeBookingStatusCommand request, CancellationToken cancellationToken)
    {
        var booking = await _db.Bookings
            .FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

        if (booking == null)
            throw new BadRequestException("ჯავშანი ვერ მოიძებნა.");

        booking.ChangeStatus(request.BookingStatus);
        await _db.SaveChangesAsync(cancellationToken);

        return new ChangeBookingStatusResponse("ჯავშნის სტატუსი წარმატებით განახლდა.");
    }
}
