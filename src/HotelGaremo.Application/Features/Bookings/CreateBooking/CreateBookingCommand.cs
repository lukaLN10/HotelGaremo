using HotelGaremo.Domain.Enums;
using MediatR;

namespace HotelGaremo.Application.Features.Bookings.CreateBooking;

public record CreateBookingCommand(
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    int CottageId,
    PaymentType PaymentType) : IRequest<CreateBookingResponse>
{
    public int UserId { get; init; }
}
