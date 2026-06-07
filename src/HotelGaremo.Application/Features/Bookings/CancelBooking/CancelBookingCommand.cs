using MediatR;

namespace HotelGaremo.Application.Features.Bookings.CancelBooking;

public record CancelBookingCommand(int BookingId) : IRequest<CancelBookingResponse>
{
    public int UserId { get; init; }
}
