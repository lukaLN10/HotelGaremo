using MediatR;

namespace HotelGaremo.Application.Features.Bookings.GetBookingById;

public record GetBookingByIdQuery(int BookingId) : IRequest<GetBookingByIdResponse>;
