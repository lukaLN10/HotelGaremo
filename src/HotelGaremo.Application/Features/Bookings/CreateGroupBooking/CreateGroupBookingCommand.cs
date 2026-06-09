using HotelGaremo.Domain.Enums;
using MediatR;

namespace HotelGaremo.Application.Features.Bookings.CreateGroupBooking;

public record CreateGroupBookingCommand(
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    List<int> CottageIds,
    PaymentType PaymentType) : IRequest<CreateGroupBookingResponse>
{
    public int UserId { get; init; }
}
