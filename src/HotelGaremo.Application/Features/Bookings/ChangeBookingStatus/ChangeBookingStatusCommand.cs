using HotelGaremo.Domain.Enums;
using MediatR;

namespace HotelGaremo.Application.Features.Bookings.ChangeBookingStatus;

public record ChangeBookingStatusCommand(int BookingId, BookingStatus BookingStatus) : IRequest<ChangeBookingStatusResponse>;
