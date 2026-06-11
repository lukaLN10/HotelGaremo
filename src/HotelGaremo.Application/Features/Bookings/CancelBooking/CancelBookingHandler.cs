using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.CancelBooking;

public class CancelBookingHandler : IRequestHandler<CancelBookingCommand, CancelBookingResponse>
{
    private readonly IDataContext _db;

    public CancelBookingHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<CancelBookingResponse> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _db.Bookings
            .FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

        if (booking == null)
            throw new BadRequestException("ჯავშანი ვერ მოიძებნა.");

        if (booking.UserId != request.UserId)
            throw new BadRequestException("თქვენ არ გაქვთ ამ ჯავშნის გაუქმების უფლება.");

        if (booking.BookingStatus == BookingStatus.CanceledByUser ||
            booking.BookingStatus == BookingStatus.CanceledByAdmin)
            throw new BadRequestException("ჯავშანი უკვე გაუქმებულია.");

        if (booking.CheckIn - DateTime.UtcNow < TimeSpan.FromDays(7))
            throw new BadRequestException("ჯავშნის გაუქმება შესაძლებელია მხოლოდ შემოსვლამდე მინიმუმ 1 კვირით ადრე.");

        if (booking.GroupBookingNumber != null)
        {
            var groupBookings = await _db.Bookings
                .Where(x => x.GroupBookingNumber == booking.GroupBookingNumber)
                .ToListAsync(cancellationToken);

            foreach (var groupBooking in groupBookings)
            {
                groupBooking.ChangeStatus(BookingStatus.CanceledByUser);
            }
        }
        else
        {
            booking.ChangeStatus(BookingStatus.CanceledByUser);
        }

        await _db.SaveChangesAsync(cancellationToken);

        return new CancelBookingResponse("ჯავშანი წარმატებით გაუქმდა.");
    }
}
