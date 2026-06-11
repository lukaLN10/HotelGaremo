using MediatR;

namespace HotelGaremo.Application.Features.Bookings.NotifyBookingPayment;

public record NotifyBookingPaymentCommand(string BookingNumber) : IRequest<NotifyBookingPaymentResponse>
{
    public int UserId { get; init; }
}
