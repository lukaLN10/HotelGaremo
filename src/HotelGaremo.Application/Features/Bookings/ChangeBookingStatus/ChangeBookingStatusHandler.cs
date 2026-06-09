using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.ChangeBookingStatus;

public class ChangeBookingStatusHandler : IRequestHandler<ChangeBookingStatusCommand, ChangeBookingStatusResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<ChangeBookingStatusCommand> _validator;

    public ChangeBookingStatusHandler(IDataContext db, IValidator<ChangeBookingStatusCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<ChangeBookingStatusResponse> Handle(ChangeBookingStatusCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var booking = await _db.Bookings
            .FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

        if (booking == null)
            throw new BadRequestException("ჯავშანი ვერ მოიძებნა.");

        booking.ChangeStatus(request.BookingStatus);
        await _db.SaveChangesAsync(cancellationToken);

        return new ChangeBookingStatusResponse("ჯავშნის სტატუსი წარმატებით განახლდა.");
    }
}
