using MediatR;

namespace HotelGaremo.Application.Features.Bookings.GetAllBookings;

public record GetAllBookingsQuery : IRequest<List<GetAllBookingsResponse>>;
